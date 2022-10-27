using Bootstrap;
using Framework.DI;
using Game.CameraBehaviour;
using UnityEngine;

namespace Game.Bootstraps
{
    public class HUDBootstrap: BasicBootstrap
    {
        [InjectField] private CameraController _cameraController;
        [SerializeField] private Camera _uiCamera;

        protected override void OnStart()
        {
            base.OnStart();

            _cameraController.AddOverlayCamera(_uiCamera);
        }
    }
}