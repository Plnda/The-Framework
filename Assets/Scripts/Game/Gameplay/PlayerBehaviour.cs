using Framework.DI;
using Game.AI;
using Game.Cameras;
using Game.Gameplay.Weapons;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerBehaviour: DependencyBehaviour
    {
        [InjectField] private CameraBehaviour _cameraBehaviour;
        [InjectField] private Controls.PlayerController _playerController;

        private Animator _animator;
        private PathfindingBehaviour _agent;
        private WeaponBehaviour _weaponBehaviour;
        
        private readonly int _velocityTag = Animator.StringToHash("Velocity");
        
        private void Start()
        {
            _cameraBehaviour.SetTarget(transform);
            _agent = gameObject.AddComponent<PathfindingBehaviour>();
            _weaponBehaviour = GetComponentInChildren<WeaponBehaviour>();
            
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            ProcessInput();
            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            _animator.SetFloat(_velocityTag, _agent.velocity);
        }
        
        private void ProcessInput()
        {
            if (Input.GetMouseButton((int) MouseButton.Right))
            {
                _agent.Stop();
                
                // Raycast towards the player
              
                
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(ray, out var hit);

                if (hit.collider == null)
                {
                    return;
                }
                
                _agent.RotateTowards(hit.point);
      
                if (_weaponBehaviour.Shoot(hit.point))
                {
                    _animator.Play("ShootSingleshot_AR");
                };
            }
            
            // If we pressed the left mouse button
            if (Input.GetMouseButton((int)MouseButton.Left))
            {
                // Raycast towards the player
                var plane = new Plane(Vector3.up, Vector3.zero);
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (!plane.Raycast(ray, out var entry))
                {
                    return;
                }
                
                _agent.Move(ray.GetPoint(entry));
            }
        }
    }
}