using Framework.DI;
using UnityEngine;

namespace Game.Controls
{
    public class PlayerController: DependencyBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            
            RegisterSelf(true);
        }

        private void Update()
        {
           
        }

        private void UpdateMovementControls()
        {

        }
    }
}