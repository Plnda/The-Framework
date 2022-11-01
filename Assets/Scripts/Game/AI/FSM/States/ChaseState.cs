using System.Linq;
using Game.Gameplay;
using UnityEngine;

namespace Game.AI.FSM.States
{
    public class ChaseState : FSMState
    {
        public ChaseState(
            PathfindingBehaviour behaviour, 
            AIBehaviour aiBehaviour
        ) : base("Chase State", behaviour, aiBehaviour) { }

        
        bool isInAttackRange => _aiBehaviour.distanceFromVisibleEnemy <= _aiBehaviour.rangeDistance;
        bool isEscaped => (int)_aiBehaviour.distanceFromVisibleEnemy > _aiBehaviour.viewDistance;
        
        public override bool CanEnterState()
        {
            return _aiBehaviour.visibleEnemy != null && !isInAttackRange;
        }

        public override void RunState()
        {
            var visibleEnemy = _aiBehaviour.visibleEnemy;
            
            if (visibleEnemy == null)
            {
                return;
            }
            
            _behaviour.SetStopRange(_aiBehaviour.rangeDistance);
            
            if (isEscaped)
            {
                _behaviour.Stop();
                return;
            }
            
            _behaviour.Move(visibleEnemy.transform.position);
        }
    }
}