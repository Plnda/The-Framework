using System;
using System.Collections.Generic;
using System.Linq;
using Framework.DI;
using Game.AI;
using Game.AI.FSM;
using Game.AI.FSM.Trees;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Controller
{
    [RequireComponent(typeof(IAstarAI))]
    public partial class AIController: DependencyBehaviour
    {
        private PathfindingController _agent;
        private FSMStateManager _stateManager;
        private Animator _animator;

        [SerializeField] private Canvas _canvas;
        
        [Header(("AI Configuration"))] 
        public float patrolRadius;
        public float viewDistance;
        public float fieldOfView;
        public float rangeDistance;

        public PlayerController visibleEnemy => GetVisiblePlayers().FirstOrDefault();
        
        public float distanceFromVisibleEnemy => Vector3.Distance(
            visibleEnemy.transform.position, 
            transform.position
        );
        
        private readonly int _velocityTag = Animator.StringToHash("Velocity");
        
        private void Start()
        {
            _agent = gameObject.AddComponent<PathfindingController>();
            _stateManager = gameObject.AddComponent<FSMStateManager>();
            _animator = GetComponent<Animator>();
            
            _agent.SetStopRange(rangeDistance);
            
            _stateManager.SetTree(new BasicAITree(_agent, this));
            _stateManager.StartTree();
        }

        public void RotateTowards(Transform target)
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = target.position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = 360 * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Update agents rotation
            _agent.SetRotation(Quaternion.LookRotation(newDirection));
        }

        private void Update()
        {
            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            _canvas.transform.LookAt(Camera.main.transform);
            _animator.SetFloat(_velocityTag, _agent.velocity);
        }

        public List<PlayerController> GetVisiblePlayers()
        {
            var rangedCollisions = Physics.OverlapSphere(transform.position, viewDistance);
            var closeCollisons = Physics.OverlapSphere(transform.position, rangeDistance);
            
            var visiblePlayers = new List<PlayerController>();

            foreach (var collision in closeCollisons)
            {
                var player = collision.GetComponent<PlayerController>();

                if (player == null)
                {
                    continue;
                }

                if (!visiblePlayers.Contains(player))
                {
                    visiblePlayers.Add(player);
                }
            }
            
            foreach (var collision in rangedCollisions)
            {
                var player = collision.GetComponent<PlayerController>();

                if (player == null || visiblePlayers.Contains(player))
                {
                    continue;
                }

                var directionToTarget = (collision.transform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < (fieldOfView / 2))
                {
                    var distance = Vector3.Distance(transform.position, collision.transform.position);
                    
                    // TODO: Maybe draw from the eyes instead of the center
                    if (Physics.Raycast(transform.position, directionToTarget, distance))
                    {
                        visiblePlayers.Add(player);
                    }
                }
            }
            
            return visiblePlayers;
        }
        
        public Vector3 GetPatrolDestination()
        {
            var point = Random.insideUnitSphere * patrolRadius;
            
            point.y = 0;
            point += transform.position;
            
            return point;
        }
    }
    
    #region UNITY_EDITOR
    #if UNITY_EDITOR
    public partial class AIController
    {
        private void OnDrawGizmos()
        {
            // Draw range indicator
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position , transform.up, viewDistance);

            // Draw range indicator
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(transform.position , transform.up, rangeDistance);
            
            UnityEditor.Handles.color = Color.red;
            float totalFOV = fieldOfView;
            float rayRange = viewDistance;
            float halfFOV = totalFOV / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis( -halfFOV, Vector3.up );
            Quaternion rightRayRotation = Quaternion.AngleAxis( halfFOV, Vector3.up );
            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay( transform.position, leftRayDirection * rayRange );
            Gizmos.DrawRay( transform.position, rightRayDirection * rayRange );
        }
    }
    
    #endif
    #endregion
}