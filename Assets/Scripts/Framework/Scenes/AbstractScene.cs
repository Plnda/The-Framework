using Framework.Services;
using UnityEngine;

namespace Framework.Scenes
{
    public abstract class AbstractScene: MonoBehaviour
    {
        private bool _isBooted;
        private bool _isStarted;
        
        protected Bootstrap.Bootstrap _bootstrap;

        public void Boot(Bootstrap.Bootstrap bootstrap)
        {
            _bootstrap = bootstrap;
            
            SetupDependencies();

            _isBooted = true;
        }

        protected async void UnloadLevel()
        { 
            OnStop();
            await _bootstrap.UnloadCurrentLevel();
        }
        
        protected virtual void SetupDependencies()
        {
        }
        
        protected virtual void OnStart()
        {
        }

        protected virtual void OnStop()
        {
        }

        protected virtual void OnUpdate()
        {
            if (!_isStarted)
            {
                _isStarted = true;
                OnStart();
            }
        }
        
        protected virtual void OnFixedUpdate()
        {
        }
        
        private void Update()
        { 
            if (!_isBooted)
            {
                return;
            }
            
            OnUpdate();
        }

        private void FixedUpdate()
        { 
            if (!_isBooted)
            {
                return;
            }
            
            OnFixedUpdate();
        }
    }
}