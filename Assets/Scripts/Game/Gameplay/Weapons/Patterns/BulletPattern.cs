using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weapons.Patterns
{
    public abstract class BulletPattern
    {
        public virtual List<RaycastHit> CreatePattern(Transform origin, Vector3 direction)
        {
            return new List<RaycastHit>();
        }
    }
}