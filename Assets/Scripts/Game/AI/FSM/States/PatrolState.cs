using Game.Controller;

namespace Game.AI.FSM.States
{
    public class PatrolState : FSMState
    {
        private float _radius;
        
        public PatrolState(
            PathfindingController controller, 
            AIController aiController, 
            float radius
        ) : base("Patrol State", controller, aiController) {
            
            _radius = radius;
        }

        public override bool CanEnterState()
        {
            return _controller.pathfindingReady && _aiController.visibleEnemy == null;
        }

        public override void RunState()
        {
            if (_aiController.visibleEnemy != null)
            {
                return;
            }
            
            _controller.SetStopRange(0.5f);
            
            var destination = _aiController.GetPatrolDestination();
            _controller.Move(destination);
        }
    }
}