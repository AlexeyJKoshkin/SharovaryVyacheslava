using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public abstract class ScriptableInstaller : ScriptableObject, IInstaller
    {
        protected IContainerBuilder Container { get; private set; }

        public void Install(IContainerBuilder builder)
        {
            Container = builder;
            InstallBindings();
        }

        protected abstract void InstallBindings();

        protected void SingletonAllInterfaces<T>()
        {
            Container.Register<T>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        
        protected void SingletonAllInterfaces<T>(T instance)
        {
            if(instance== null) return;
            Container.RegisterInstance(instance);
        }

      
    }
}