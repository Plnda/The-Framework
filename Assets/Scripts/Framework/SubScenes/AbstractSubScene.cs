using UnityEngine;

namespace Framework.SubScenes
{
    public abstract class AbstractSubScene<T> : MonoBehaviour where T : Bootstrap.Bootstrap
    {
        private bool _isBooted;
        private bool _isStarted;
        
        public virtual string title { get; }
    
        protected T _bootstrap;

        public void Boot(T bootstrap)
        {
            _bootstrap = bootstrap;
            
            LoadDependencies(_bootstrap);
            
            _isBooted = true;
        }

        public void DeActivate()
        {
            _isBooted = false;
        }

        protected virtual void LoadDependencies(T bootstrap)
        {
          
        }

        protected virtual void OnStart()
        {
            
        }

        private void OnUpdate()
        {
            if (!_isStarted)
            {
                _isStarted = true;
                OnStart();
            }
        }
        
        private void Update()
        {
            if (!_isBooted)
            {
                return;
            }

            OnUpdate();
        }
    }
}