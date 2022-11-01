using System.Collections.Generic;
using Game.Gameplay;

namespace Game.AI.FSM
{
    public abstract class FSMStateTree
    {
        /// <summary>
        /// The controller that will be used in this state machine
        /// </summary>
        protected PathfindingBehaviour Behaviour;
        
        /// <summary>
        /// The AI controller that can take actions
        /// </summary>
        protected AIBehaviour _aiController;

        /// <summary>
        /// The states that this tree has
        /// </summary>
        protected List<FSMState> _states;
        
        /// <summary>
        /// Public exposure
        /// </summary>
        public List<FSMState> states => _states;
    }
}