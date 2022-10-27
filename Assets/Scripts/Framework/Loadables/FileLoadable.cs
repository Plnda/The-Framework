using System.Threading.Tasks;
using Loadables;
using UnityEngine;

namespace Framework.Loadables
{
    [CreateAssetMenu(fileName = "Asset", menuName = "Assets/Create File Asset")]
    public class FileLoadable: AbstractAssetLoadable
    {
        private TextAsset _loadedAsset;
        
        public override async Task Load()
        {
            await base.Load();

            if (_loadedAsset != null)
            {
                return;
            }
            
            _loadedAsset = await Load<TextAsset>();
            
            Debug.Log($"Loaded Asset: {key}");
        }

        public string text => _loadedAsset.text;
    }
}