using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bootstrap;
using Framework.Loadables;
using Loadables;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Framework.Levels
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level/Create Level")]
    public partial class Level: ScriptableObject
    {
        [SerializeReference] private List<AbstractAssetLoadable> _sceneAssets = new List<AbstractAssetLoadable>();
        
        [SerializeField] private AssetReference _sceneReference;
        [SerializeField] private LevelType _levelType;

        public UnityEvent OnLevelLoad;
        public UnityEvent OnLevelUnload;

        private SceneInstance _instance;
        
        private Scene _loadedSceneReference;
        private Level _parent;

        private bool _setActive = false;
        
        private async Task LoadScene(bool setActive, bool activate = true)
        {
            if (_sceneReference.IsValid() && _sceneReference.IsDone)
            {
                _sceneReference.ReleaseAsset();
            }

            _setActive = setActive;
            
            var handle = _sceneReference.LoadSceneAsync(LoadSceneMode.Additive, activate);
            handle.Completed += OnComplete;
            
            await handle.Task;

            _instance = handle.Result;
        }
        
        private void OnComplete(AsyncOperationHandle<SceneInstance> result)
        {
            result.Completed -= OnComplete;
            
            _loadedSceneReference = result.Result.Scene;

            if (_setActive)
            {
                SceneManager.SetActiveScene(result.Result.Scene);
            }
        }
        
        private async Task LoadAssets()
        {
            var handles = _sceneAssets.Select(x => x.Load());
            
            foreach (var handle in handles)
            {
                await handle;
            }
        }

        private void LoadBootstrap()
        {
            var bootstrap = FindObjectsOfType<Bootstrap.Bootstrap>()
                .FirstOrDefault(x => x.Identifier == _levelType);

            if (bootstrap == null)
            {
                throw new Exception("Couldn't find bootstrap");
            }
            
            bootstrap.Boot(this);
        }
        
        private void UnloadAssets()
        {
            _sceneAssets.ForEach(x => x.Unload());
            Debug.Log($"Unloaded Assets from level {name}");
        }
        
        private async Task UnloadScene()
        {
            await _sceneReference.UnLoadScene().Task;
        }
    }

    /// <summary>
    /// MARK: Public methods
    /// </summary>
    public partial class Level
    {
        private IEnumerator ActivateLevelCoroutine(Action completion)
        {
            var instance = _instance.ActivateAsync();

            while (!instance.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            
            completion?.Invoke();
        } 
        
        public async Task ActivateLevel(MonoBehaviour runner)
        {
            if (_instance.Equals(default))
            {
                return;
            }

            var source = new TaskCompletionSource<bool>();

            Action completion = () =>
            {
                source.SetResult(true);
            };
            
            runner.StartCoroutine(ActivateLevelCoroutine(completion));

            await source.Task;
            
            SetSceneActive();
            LoadBootstrap();
        }
        
        public T FetchAsset<T>(string key)
        {
            var asset = _sceneAssets.FirstOrDefault(x => x.Key == key);

            if (asset is not T loadable)
            {
                throw new Exception($"Resource {key} not found");
            }

            return loadable;
        }
        
        public GameObject Spawn(string key, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var asset = _sceneAssets.FirstOrDefault(x => x.Key == key);

            if (asset is not GameObjectLoadable loadable)
            {
                return null;
            }
            
            return loadable.Instantiate(position, rotation, parent);
        }

        public async Task PreloadAssets()
        {
            await LoadAssets();
        }
        
        /// <summary>
        /// Allows you to load a level in the game, provide a parent to reactivate it when this level unloads
        /// Handy when you temporarily need to load a scene
        /// </summary>
        /// <param name="setActive">If we want to activate the scene immediately on load</param>
        /// <param name="parent">Provide a parent to set parent scene active on unload</param>
        public async Task Load(bool setActive = true, bool activate = true, Level parent = null)
        {
            if (parent != null)
            {
                OnLevelUnload?.AddListener(parent.SetSceneActive);
            }

            _parent = parent;

            await LoadAssets();
            await LoadScene(setActive, activate);


            if (activate)
            {
                LoadBootstrap();

                OnLevelLoad?.Invoke();
                OnLevelLoad?.RemoveAllListeners();
            }
        }

        public async Task Load(Level parent)
        {
           await Load(true, parent);
        }
 
        public void SetSceneActive()
        {
            SceneManager.SetActiveScene(_loadedSceneReference);
        }

        public async Task UnloadParent()
        {
            if (_parent != null)
            {
                await _parent.UnloadLevel();
            }
        }

        public async Task UnloadLevelAndParent()
        {
            await UnloadLevel();
            await UnloadParent();
        }
        
        public async Task UnloadLevel()
        {
            UnloadAssets();
            
            OnLevelUnload?.Invoke();
            OnLevelUnload?.RemoveAllListeners();
            
            await UnloadScene();
        }
    }
}