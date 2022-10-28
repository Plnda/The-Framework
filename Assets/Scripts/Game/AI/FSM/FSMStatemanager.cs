using System.Collections.Generic;
using System.Linq;
using Game.AI.FSM.States;
using Game.Controller;
using UnityEngine;

namespace Game.AI.FSM
{
    public class FSMStateManager: MonoBehaviour
    {
        private FSMStateTree _tree;
        private bool _started;

        public void SetTree(FSMStateTree tree)
        {
            _tree = tree;
        }

        public void StartTree()
        {
            _started = true;
        }

        public void StopTree()
        {
            _started = false;
        }
        
        private void ProcessStateMachine()
        {
            foreach (var state in _tree.states)
            {
                if (state.CanEnterState())
                {
                    Debug.Log($"Running state {state.name}");
                    state.RunState();
                }
            }
        }

        private void Update()
        {
            if (_started)
            {
                ProcessStateMachine();
            }
        }
    }
}