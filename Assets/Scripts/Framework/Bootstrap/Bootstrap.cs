using System;
using System.Threading.Tasks;
using Framework;
using Framework.Levels;
using Framework.Scenes;
using Framework.Services;
using Loadables;
using UnityEngine;
using UnityEngine.Events;

namespace Bootstrap
{
    public abstract class Bootstrap: MonoBehaviour
    {
        private bool _isBooted;
        private bool _isStarted;

        protected Level _currentLevel;
        
        public UnityEvent OnBootstrapStart = new UnityEvent();

        public AbstractScene Scene;
        public LevelType Identifier;
        
        /// <summary>
        /// Boots the bootstrap so it will start running
        /// </summary>
        /// <param name="currentLevel">The current level that this bootstrap is associated with</param>
        public void Boot(Level currentLevel)
        {
            _currentLevel = currentLevel;
            _isBooted = true;
        }

        /// <summary>
        /// Fetches an asset that is stored in the current level
        /// </summary>
        /// <param name="key">The key of the asset we are trying to load</param>
        /// <returns></returns>
        public T FetchAsset<T>(string key)
        {
            return _currentLevel.FetchAsset<T>(key);
        }
        
        public async Task UnloadCurrentLevel()
        {
            await _currentLevel.UnloadLevel();
        }

        private void Awake()
        {
            ServiceManager.shared.Resolve(gameObject);
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }

        protected virtual void OnStart()
        {
            OnBootstrapStart?.Invoke();
        }
        
        protected virtual void OnUpdate()
        {
            if (!_isStarted)
            {
                _isStarted = true;
                OnStart();
            }
        }
        
        protected virtual void OnFixedUpdate()
        {
            
        }
        
        private void Update()
        { 
            if (!_isBooted)
            {
                return;
            }
            
            OnUpdate();
        }

        private void FixedUpdate()
        { 
            if (!_isBooted)
            {
                return;
            }
            
            OnFixedUpdate();
        }
    }
}