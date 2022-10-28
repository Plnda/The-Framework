using System.Linq;
using Game.Controller;
using UnityEngine;

namespace Game.AI.FSM.States
{
    public class ChaseState : FSMState
    {
        public ChaseState(
            PathfindingController controller, 
            AIController aiController
        ) : base("Chase State", controller, aiController) { }

        
        bool isInAttackRange => _aiController.distanceFromVisibleEnemy <= _aiController.rangeDistance;
        bool isEscaped => _aiController.distanceFromVisibleEnemy >= _aiController.viewDistance;
        
        public override bool CanEnterState()
        {
            return _aiController.visibleEnemy != null && !isInAttackRange;
        }

        public override void RunState()
        {
            var visibleEnemy = _aiController.visibleEnemy;
            
            if (visibleEnemy == null)
            {
                return;
            }
            
            _controller.SetStopRange(_aiController.rangeDistance);
            
            if (isEscaped)
            {
                _controller.Stop();
                return;
            }
            
            _controller.Move(visibleEnemy.transform.position);
        }
    }
}