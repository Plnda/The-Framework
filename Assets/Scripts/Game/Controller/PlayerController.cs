using System;
using Framework.DI;
using Game.AI;
using Game.CameraBehaviour;
using Game.Controls;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Controller
{
    public class PlayerController: DependencyBehaviour
    {
        [InjectField] private CameraController _cameraController;
        [InjectField] private InputController _inputController;

        [SerializeField] private GameObject _muzzleFlash;
        
        private Animator _animator;
        private PathfindingController _agent;

        private readonly int _velocityTag = Animator.StringToHash("Velocity");
        
        private void Start()
        {
            _cameraController.SetTarget(transform);
            _agent = gameObject.AddComponent<PathfindingController>();
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
            if (Input.GetMouseButtonDown((int) MouseButton.Right))
            {
                _muzzleFlash.SetActive(false);
                _muzzleFlash.SetActive(true);
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