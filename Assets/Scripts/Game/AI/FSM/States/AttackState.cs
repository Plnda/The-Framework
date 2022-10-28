using Game.Controller;

namespace Game.AI.FSM.States
{
    public class AttackState: FSMState
    {
        public AttackState(
            PathfindingController controller, 
            AIController aiController
        ) : base("Attack State", controller, aiController) { }


        public override bool CanEnterState()
        {
            return _aiController.visibleEnemy && 
                   _aiController.distanceFromVisibleEnemy <= _aiController.rangeDistance;
        }

        public override void RunState()
        {
            var visibleEnemy = _aiController.visibleEnemy;

            if (visibleEnemy == null)
            {
                return;
            }
            
            _controller.Move(visibleEnemy.transform.position);

            if (_controller.velocity < 0.001f)
            {
                _aiController.RotateTowards(visibleEnemy.transform);
            }
        }
    }
}