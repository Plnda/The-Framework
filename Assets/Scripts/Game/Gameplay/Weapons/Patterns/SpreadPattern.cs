using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weapons.Patterns
{
    public class SpreadPattern: BulletPattern
    {
        private readonly int _pellets;
        private readonly int _dispersionAngle;
        private readonly int _range;
               
        public SpreadPattern(int pellets, int dispersionAngle, int range)
        {
            _pellets = pellets;
            _dispersionAngle = dispersionAngle;
            _range = range;
        }
        
        public override List<RaycastHit> CreatePattern(Transform origin, Vector3 direction)
        {
            List<RaycastHit> hits = new List<RaycastHit>();
            
            for (int i = 0; i < _pellets; i++)
            {
                Vector3 scatterDirection = Random.insideUnitCircle * _dispersionAngle;
                scatterDirection.z = _range;
                scatterDirection = origin.TransformDirection(scatterDirection);

                Physics.Raycast(origin.position, scatterDirection, out RaycastHit hitinfo, _range);
                
                hits.Add(hitinfo);

                Debug.DrawRay(origin.position, scatterDirection, Color.green, 5f);
            }
            
            return hits;
        }
    }
}