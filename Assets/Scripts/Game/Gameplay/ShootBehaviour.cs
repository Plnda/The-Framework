using Framework.DI;
using Game.Bootstrap;
using Game.Gameplay.Weapons.Patterns;
using Loadables;
using UnityEngine;

namespace Game.Gameplay
{
    public class ShootBehaviour : DependencyBehaviour
    {
        [InjectField] private GameBootstrap _bootstrap;

        private GameObjectLoadable _dirtImpactAsset => _bootstrap.FetchAsset<GameObjectLoadable>("DirtImpact");
        
        private MuzzleFlash _muzzleFlash;
        
        private void Start()
        {
            _muzzleFlash = GetComponentInChildren<MuzzleFlash>(true);
            _muzzleFlash.gameObject.SetActive(false);
        }

        public void Shoot(BulletPattern pattern, Vector3 position)
        {
            _muzzleFlash.gameObject.SetActive(false);
            _muzzleFlash.gameObject.SetActive(true);

            var direction = position - transform.position;

            var hits = pattern.CreatePattern(transform, direction);

            foreach (var hit in hits)
            {
                if (hit.collider == null)
                {
                    continue;
                }

                var impact = _dirtImpactAsset.Instantiate(
                    hit.point, 
                    Quaternion.LookRotation(hit.normal), 
                    null
                );
                
                Destroy(impact, 5.0f);
            }
        }
    }
}
