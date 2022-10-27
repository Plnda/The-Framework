using Framework.DI;
using UnityEngine;

namespace Game.Controls
{
    public class InputController: DependencyBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            
            RegisterSelf(true);
        }

        private void Update()
        {
            UpdateMovementControls();
        }

        private void UpdateMovementControls()
        {

        }
    }
}