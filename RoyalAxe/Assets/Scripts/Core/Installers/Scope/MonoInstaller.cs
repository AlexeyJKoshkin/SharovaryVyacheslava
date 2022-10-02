using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        protected IContainerBuilder Container { get; private set; }

        public virtual void Install(IContainerBuilder builder)
        {
            Container = builder;
            InstallBindings();
        }

        protected abstract void InstallBindings();
    }
}