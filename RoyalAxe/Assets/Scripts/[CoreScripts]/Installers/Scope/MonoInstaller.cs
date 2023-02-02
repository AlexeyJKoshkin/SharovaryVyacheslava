using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        protected IContainerBuilder Container { get; private set; }

        [SerializeField]
        private bool _used = true;

        public virtual void Install(IContainerBuilder builder)
        {
            if(!_used) return;
            Container = builder;
            InstallBindings();
        }

        protected abstract void InstallBindings();
    }
}