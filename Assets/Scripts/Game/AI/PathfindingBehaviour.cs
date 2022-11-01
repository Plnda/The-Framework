using System;
using UnityEngine;
using Pathfinding;
    
namespace Game.AI
{
    [RequireComponent(typeof(IAstarAI))]
    public class PathfindingBehaviour: MonoBehaviour
    {
        private RichAI _ai;

        public float velocity => _ai.velocity.normalized.magnitude;
        public bool pathfindingReady =>(_ai.reachedEndOfPath || !_ai.hasPath);

        private void Awake()
        {
            _ai = GetComponent<RichAI>();
        }

        private void Update()
        {
            HandlePathfinding();
        }

        private void HandlePathfinding()
        {
            if (pathfindingReady && !_ai.pathPending)
            {
                _ai.SearchPath();
            }
        }

        public void RotateTowards(Vector3 position)
        {

            // Dont look up or down
            position.y = transform.position.y;
            
            // Determine which direction to rotate towards
            Vector3 targetDirection = position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = 360 * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Update agents rotation
            SetRotation(Quaternion.LookRotation(newDirection));
        }
        
        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
            _ai.rotation = rotation;
        }

        public void SetStopRange(float range)
        {
            _ai.endReachedDistance = range;
        }
        
        public void Move(Vector3 position)
        {
            _ai.destination = position;
        }

        public void Stop()
        {
            _ai.destination = transform.position;
        }
    }
}