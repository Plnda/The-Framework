using Game.Gameplay.Weapons.Patterns;
using UnityEngine;

namespace Game.Gameplay.Weapons
{
    public class WeaponBehaviour: MonoBehaviour
    {
        [SerializeField]
        private float _roundsPerMinute = 40;
        
        private BulletPattern _pattern;
        private ShootBehaviour _shootBehaviour;
        private float _nextShootTime = 0.0f;
        
        private void Start()
        {
            _pattern = new SpreadPattern(
            12, 
            1, 
            5
            );

            _shootBehaviour = gameObject.AddComponent<ShootBehaviour>();
        }

        public bool Shoot(Vector3 position)
        {
            if (Time.time > _nextShootTime)
            {
                _shootBehaviour.Shoot(_pattern, position);
                _nextShootTime = Time.time + (1 / (_roundsPerMinute / 60));
                return true;
            }

            return false;
        }
    }
}