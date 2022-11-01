using Game.Gameplay;

namespace Game.AI.FSM
{
    public abstract class FSMState
    {
        protected PathfindingBehaviour _behaviour;
        protected AIBehaviour _aiBehaviour;
        
        public string name { get; }

        protected FSMState(string name, PathfindingBehaviour behaviour, AIBehaviour aiBehaviour)
        {
            this.name = name;
            _behaviour = behaviour;
            _aiBehaviour = aiBehaviour;
        }

        public virtual bool CanEnterState()
        {
            return true;
        }

        public virtual void RunState()
        {
            
        }
    }
}