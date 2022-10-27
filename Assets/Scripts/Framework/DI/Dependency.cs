using System;
using UnityEngine;

namespace Framework.DI
{
    public struct Dependency
    {
        public Type Type { get; set; }
        public DependencyFactory.Delegate Factory { get; set; }
        public bool IsSingleton { get; set; }

        public static Dependency Register<T>(T instance, bool singleton = false) where T : MonoBehaviour
        {
            return new Dependency
            {
                Factory = DependencyFactory.FromPrefab(instance),
                Type = instance.GetType(),
                IsSingleton = singleton
            };
        }
    }
}