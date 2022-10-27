using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Loadables;
using UnityEngine;

namespace Loadables
{
    [CreateAssetMenu(fileName = "Asset", menuName = "Assets/Create GameObject Asset")]
    public class GameObjectLoadable: AbstractAssetLoadable
    {
        private GameObject _loadedAsset;
        private readonly List<GameObject> _references = new List<GameObject>();

        public override async Task Load()
        {
            await base.Load();

            if (_loadedAsset != null)
            {
                return;
            }
            
            _loadedAsset = await Load<GameObject>();;
            
            Debug.Log($"Loaded Asset: {key}");
        }

        public override void Unload()
        {
            DestroyInstances();
            
            base.Unload();
        }

        public void DestroyInstances()
        {
            _references.ForEach(Destroy);
            _references.Clear();
        }
        
        public GameObject InstantiateUI(Transform parent, bool worldPositionStays = false)
        {
            if (_loadedAsset == null)
            {
                throw new Exception($"Asset not loaded exception: {key}");
            }

            var instance = Instantiate(_loadedAsset, parent, worldPositionStays);
            
            _references.Add(instance);

            return instance;
        }
        
        public T InstantiateUI<T>(Transform parent, bool worldPositionStays = false)
        {
            if (_loadedAsset == null)
            {
                throw new Exception($"Asset not loaded exception: {key}");
            }

            var instance = Instantiate(_loadedAsset, parent, worldPositionStays);
            
            _references.Add(instance);

            return instance.GetComponent<T>();;
        }
        
        public T Instantiate<T>(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (_loadedAsset == null)
            {
                throw new Exception($"Asset not loaded exception: {key}");
            }

            var instance = Instantiate(_loadedAsset, position, rotation, parent);
            
            _references.Add(instance);

            return instance.GetComponent<T>();
        }
        
        public GameObject Instantiate(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (_loadedAsset == null)
            {
                throw new Exception($"Asset not loaded exception: {key}");
            }

            var instance = Instantiate(_loadedAsset, position, rotation, parent);
            
            _references.Add(instance);

            return instance;
        }
    }
}