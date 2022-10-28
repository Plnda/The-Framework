using System;
using Game.Controller;
using UnityEngine;
using Pathfinding;
    
namespace Game.AI
{
    [RequireComponent(typeof(IAstarAI))]
    public class PathfindingController: MonoBehaviour
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