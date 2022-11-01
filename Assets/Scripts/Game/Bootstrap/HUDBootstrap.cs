using Bootstrap;
using Framework.DI;
using Game.Cameras;
using UnityEngine;

namespace Game.Bootstrap
{
    public class HUDBootstrap: BasicBootstrap
    {
        [InjectField] private CameraBehaviour _cameraBehaviour;
        [SerializeField] private Camera _uiCamera;

        protected override void OnStart()
        {
            base.OnStart();

            _cameraBehaviour.AddOverlayCamera(_uiCamera);
        }
    }
}