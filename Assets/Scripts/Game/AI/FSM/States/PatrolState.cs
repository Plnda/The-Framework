using Game.Gameplay;

namespace Game.AI.FSM.States
{
    public class PatrolState : FSMState
    {
        private float _radius;
        
        public PatrolState(
            PathfindingBehaviour behaviour, 
            AIBehaviour aiBehaviour, 
            float radius
        ) : base("Patrol State", behaviour, aiBehaviour) {
            
            _radius = radius;
        }

        public override bool CanEnterState()
        {
            return _behaviour.pathfindingReady && _aiBehaviour.visibleEnemy == null;
        }

        public override void RunState()
        {
            if (_aiBehaviour.visibleEnemy != null)
            {
                return;
            }
            
            _behaviour.SetStopRange(0.5f);
            
            var destination = _aiBehaviour.GetPatrolDestination();
            _behaviour.Move(destination);
        }
    }
}