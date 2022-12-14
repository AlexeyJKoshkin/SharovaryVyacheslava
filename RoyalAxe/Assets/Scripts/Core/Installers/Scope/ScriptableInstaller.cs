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
    }
}