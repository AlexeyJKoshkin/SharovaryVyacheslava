using System.Linq;
using GameKit;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public abstract class ContextScope : LifetimeScope
    {
        [SerializeField] private ScriptableInstaller[] _installers;

        [SerializeField] private MonoInstaller[] _monInstallers;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            _installers.Where(e=>e != null).ForEach(e => e.Install(builder));
            _monInstallers.Where(e=>e != null).ForEach(e => e.Install(builder));
        }
    }
}