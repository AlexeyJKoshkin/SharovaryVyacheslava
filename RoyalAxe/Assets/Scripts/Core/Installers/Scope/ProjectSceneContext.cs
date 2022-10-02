using Core.Data.Provider;
using GameKit;
using UnityEngine;
using VContainer;

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
            builder.RegisterInstance(_settings.State).AsImplementedInterfaces().AsSelf();
            BindSceneSystemsLauncher(builder);
            builder.RegisterBuildCallback(OnPostResolveHandler);
        }

        private void BindSceneSystemsLauncher(IContainerBuilder containerBuilder)
        {
            containerBuilder.Register<StateInfrastructure>(Lifetime.Singleton).AsImplementedInterfaces();
            containerBuilder.Register<RoyalAxeFeatureBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            _settings.AllSystems().ForEach(e => containerBuilder.Register(e, Lifetime.Singleton).AsSelf().AsImplementedInterfaces());
            containerBuilder.RegisterInstance(_settings).AsImplementedInterfaces().AsSelf();
        }

        private void OnPostResolveHandler(IObjectResolver objectResolver)
        {
            objectResolver.Inject(_settings.State);
            LunchScene(_settings.State);
        }

        private void LunchScene(AbstractSceneStateScriptable launcher)
        {
            HLogger.LogInfo($"Launch Scene {launcher.NodeName}");
            launcher.ActivateScene(SceneData); // активация сцены
        }
    }
}