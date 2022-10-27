using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

#pragma warning disable CS1998

namespace Framework.Loadables
{
    public abstract class AbstractAssetLoadable: ScriptableObject
    {
        [SerializeField] protected AssetReference _reference;
        [SerializeField] protected string key;

        public string Key => key;
        
        protected Task<T> Load<T>()
        {
            return _reference.LoadAssetAsync<T>().Task;
        }

        public virtual async Task Load()
        {
            Debug.Log($"Loading Asset: {key}");
        }

        public virtual void Unload()
        {
            _reference.ReleaseAsset();
        }
    }
}