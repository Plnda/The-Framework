using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weapons.Patterns
{
    public class SingleShotPattern: BulletPattern
    {
        public override List<RaycastHit> CreatePattern(Transform origin, Vector3 direction)
        {
            Physics.Raycast(new Ray {
                direction = direction,
                origin = origin.position
            }, out var result);

            Debug.DrawRay(origin.position, direction, Color.green, 5f);
            
            return new List<RaycastHit>
            {
                result
            };
        }
    }
}