using System;
using Framework.Levels;
using UnityEngine;

namespace Framework.Services
{
    [DefaultExecutionOrder(-2)]
    public class CoreService: MonoBehaviour
    {
        public static CoreService shared;
        
        [SerializeField] private Level _level;
  
        private void Awake()
        {
            shared = this;
        }

        public async void Start()
        {
            await _level.Load();
        }
    }
}