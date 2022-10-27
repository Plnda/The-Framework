using System;
using Game.Controller;
using UnityEngine;
using Pathfinding;
    
namespace Game.AI
{
    public class PathfindingController: MonoBehaviour
    {
        private IAstarAI _ai;

        public float velocity => _ai.velocity.normalized.magnitude;

        private void Start()
        {
            _ai = GetComponent<IAstarAI>();
        }

        private void Update()
        {
            HandlePathfinding();
        }

        private void HandlePathfinding()
        {
            if (!_ai.pathPending && (_ai.reachedEndOfPath || !_ai.hasPath))
            {
                _ai.SearchPath();
            }
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