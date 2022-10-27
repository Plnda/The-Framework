using System.Threading.Tasks;
using Loadables;
using UnityEngine;

namespace Framework.Loadables
{
    [CreateAssetMenu(fileName = "Asset", menuName = "Assets/Create Sprite Asset")]
    public class SpriteAssetLoadable: AbstractAssetLoadable
    {
        private Sprite _loadedAsset;
        
        public override async Task Load()
        {
            await base.Load();

            if (_loadedAsset != null)
            {
                return;
            }
            
            _loadedAsset = await Load<Sprite>();
            
            Debug.Log($"Loaded Asset: {key}");
        }

        public Sprite sprite => _loadedAsset;
    }
}