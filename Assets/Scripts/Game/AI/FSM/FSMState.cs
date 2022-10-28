using Game.Controller;

namespace Game.AI.FSM
{
    public abstract class FSMState
    {
        protected PathfindingController _controller;
        protected AIController _aiController;
        
        public string name { get; }

        protected FSMState(string name, PathfindingController controller, AIController aiController)
        {
            this.name = name;
            _controller = controller;
            _aiController = aiController;
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