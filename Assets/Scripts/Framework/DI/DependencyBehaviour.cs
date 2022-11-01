using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Framework.DI
{
    public abstract class DependencyBehaviour: MonoBehaviour
    {
        protected virtual void Awake()
        {
            if (ServiceManager.shared == null)
            {
                return;
            }
            
            ServiceManager.shared.Resolve(gameObject);
        }

        public void RegisterSelf(bool singleton)
        {
            ServiceManager.shared.RegisterDependency(new Dependency()
            {
                Factory = DependencyFactory.FromGameObject(this),
                Type = GetType(),
                IsSingleton = singleton
            });
        }
    }
}