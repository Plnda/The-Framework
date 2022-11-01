using Framework.DI;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game.Cameras
{
    public abstract partial class AbstractCameraBehaviour: DependencyBehaviour
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

    public partial class AbstractCameraBehaviour
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

        public void AddOverlayCamera(UnityEngine.Camera overlayCamera)
        {
            var cameraData = UnityEngine.Camera.main.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(overlayCamera);
        }
    }
    
    public partial class AbstractCameraBehaviour 
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