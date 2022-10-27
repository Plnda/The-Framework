using Framework.DI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game.CameraBehaviour
{
    public abstract partial class AbstracCameraController: DependencyBehaviour
    {
        private Transform _target;

        private float _movementTime = 5.0f;

        protected override void Awake()
        {
            RegisterSelf(true);
        }

        public void LateUpdate()
        {
           UpdateCameraMovement();
        }
    }

    public partial class AbstracCameraController
    {
        public void UpdateCameraMovement()
        {
            if (_target == null)
            {
                return;
            }
            
            transform.position = Vector3.Lerp(
                transform.position, 
                _target.position, 
                Time.deltaTime * _movementTime
            );
        }

        public void AddOverlayCamera(Camera overlayCamera)
        {
            var skyboxCache = RenderSettings.skybox;
            
            var cameraData = Camera.main.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(overlayCamera);

            RenderSettings.skybox = skyboxCache;
        }
    }
    
    public partial class AbstracCameraController 
    {
        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void ReleaseTarget()
        {
            _target = null;
        }
    }
}