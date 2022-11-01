using Game.Gameplay;
using UnityEngine;

namespace Game.AI.FSM.States
{
    public class AttackState: FSMState
    {
        public AttackState(
            PathfindingBehaviour behaviour, 
            AIBehaviour aiBehaviour
        ) : base("Attack State", behaviour, aiBehaviour) { }


        public override bool CanEnterState()
        {
            return _aiBehaviour.visibleEnemy && 
                   _aiBehaviour.distanceFromVisibleEnemy <= _aiBehaviour.rangeDistance;
        }

        public override void RunState()
        {
            var visibleEnemy = _aiBehaviour.visibleEnemy;

            if (visibleEnemy == null)
            {
                return;
            }
            
            _behaviour.Move(visibleEnemy.transform.position);

            if (Mathf.Approximately(_behaviour.velocity, 0))
            {
                _behaviour.RotateTowards(visibleEnemy.transform.position);
                _aiBehaviour.Shoot(visibleEnemy.transform.position);
            }
        }
    }
}