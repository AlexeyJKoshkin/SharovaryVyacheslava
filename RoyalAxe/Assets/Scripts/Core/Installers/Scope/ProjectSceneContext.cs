using Core.Data.Provider;
using GameKit;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Launcher
{
    public sealed class ProjectSceneContext : ContextScope
    {
        /// <summary>
        ///     провайдер данных для данных которые нужны только на этой сцене (условные префабы мобов)
        /// </summary>
        public DataBox[] SceneData;

        [SerializeReference] private ProjectStateSettings _settings;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            _settings.CreateFeatureBlanks();
        
            BindSceneSystemsLauncher(builder);
            builder.RegisterInstance(SceneData).AsSelf();
            builder.RegisterEntryPoint<StateSceneLauncherEntryPoint>();
        }

        private void BindSceneSystemsLauncher(IContainerBuilder containerBuilder)
        {
            containerBuilder.Register<RoyalAxeFeatureBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            _settings.AllSystems().ForEach(e => containerBuilder.Register(e, Lifetime.Singleton).AsSelf().AsImplementedInterfaces());
            containerBuilder.RegisterInstance(_settings).AsImplementedInterfaces().AsSelf();
        }


    }
}
