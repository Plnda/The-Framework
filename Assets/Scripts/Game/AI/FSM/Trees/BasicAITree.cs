using System.Collections.Generic;
using Game.AI.FSM.States;
using Game.Gameplay;

namespace Game.AI.FSM.Trees
{
    public class BasicAITree : FSMStateTree
    {
        public BasicAITree(PathfindingBehaviour behaviour, AIBehaviour aiController)
        {
            Behaviour = behaviour;
            _aiController = aiController;
            
            _states = new List<FSMState>()
            {
                new PatrolState(
                    Behaviour, 
                    _aiController, 
                    15.0f
                ),
                new ChaseState(
                    Behaviour, 
                    _aiController
                ),
                new AttackState(
                    Behaviour, 
                    _aiController
                )
            };
        }
    }
}