using System.Collections.Generic;
using Game.AI.FSM.States;
using Game.Controller;

namespace Game.AI.FSM.Trees
{
    public class BasicAITree : FSMStateTree
    {
        public BasicAITree(PathfindingController controller, AIController aiController)
        {
            _controller = controller;
            _aiController = aiController;
            
            _states = new List<FSMState>()
            {
                new PatrolState(
                    _controller, 
                    _aiController, 
                    15.0f
                ),
                new ChaseState(
                    _controller, 
                    _aiController
                ),
                new AttackState(
                    _controller, 
                    _aiController
                )
            };
        }
    }
}