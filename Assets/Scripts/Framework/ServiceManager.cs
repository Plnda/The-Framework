using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.DI;
using UnityEngine;

namespace Framework
{
    [DefaultExecutionOrder(-2)]
    public class ServiceManager : MonoBehaviour
    {
        public static ServiceManager shared;

        [SerializeReference] private List<MonoBehaviour> _dependencies = new(); 
        
        private readonly DependenciesCollection _collection = new();
        private DependenciesProvider _provider;

        private void Awake()
        {
            if (shared != null)
            {
                Destroy(this);
                return;
            }

            shared = this;
            
            DontDestroyOnLoad(gameObject);
            
            SetupGraph();
        }
        
        private void SetupGraph()
        {
            foreach (var dependency in _dependencies)
            {
                RegisterDependency(dependency, false);
            }
        }

        public void Resolve(GameObject obj)
        {
            _provider = new DependenciesProvider(_collection);

            var children = obj.GetComponentsInChildren<MonoBehaviour>(true);

            foreach (var child in children)
            {
                _provider.Inject(child);
            }
        }

        public void RegisterDependency(Dependency dependency)
        {
            print($"Register {dependency.Type}");
            _collection.Add(dependency);
        }
        
        public void RegisterDependency<T>(T instance, bool singleton = false) where T: MonoBehaviour
        {
            RegisterDependency(Dependency.Register(instance, singleton));
        }
    }
}