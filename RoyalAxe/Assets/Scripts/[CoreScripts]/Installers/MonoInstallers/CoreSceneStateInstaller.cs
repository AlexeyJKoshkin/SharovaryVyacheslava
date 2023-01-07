using Core;
using Core.Launcher;
using RoyalAxe.CoreLevel;
using RoyalAxe.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace RoyalAxe
{
    public class CoreSceneStateInstaller : MonoInstaller
    {
        [SerializeField] private LevelInfrastructureView _levelInfrastructureView;
        [FoldoutGroup("UI")] [SerializeField] private CoreGameSceneUIView _coreGameSceneUiView;
        [FoldoutGroup("UI")] [SerializeField] private BuffSelectWindowView _buffSelectWindowView;
        [FoldoutGroup("UI")] [SerializeField] private WinWindowView _winWindowView;
        [FoldoutGroup("UI")] [SerializeField] private LoseWindowView _loseWindowView;
        
        [FoldoutGroup("Debug")] [SerializeField] private CoreDebugWaveInfoUIView _debugWindowView;

        protected override void InstallBindings()
        {
            
            Container.Register<CoreGameState>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            Container.Register<CoreGameUtility>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            Container.Register<CoreGameSceneInfrastructure>(Lifetime.Singleton).AsImplementedInterfaces();
            
            
            Container.RegisterInstance(_levelInfrastructureView).AsSelf();
            
            Container.Register<SpawnWizardFacade>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UIScenarioExecutor>(Lifetime.Singleton).AsImplementedInterfaces();
            BindScenarios();
            BindUICommands();
            BindUIViews();
        }

        private void BindUICommands()
        {
            Container.Register<StopCoreGameLogicCommand>(Lifetime.Singleton).AsImplementedInterfaces();
            
            
            
            Container.Register<DefaultCoreGameUIPrepareCommand>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<DebugCoreGameUICommandPrepare>(Lifetime.Singleton).AsImplementedInterfaces();
            
            Container.Register<PrepareGameUICommandComposite>(Lifetime.Singleton).As<IPrepareGameUICommandComposite>();
            

            Container.Register<ResetCoreGameToRetryCommand>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindScenarios()
        {
            Container.Register<SelectBuffScenario>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<ShowSelectBuffWindowCommand>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<WinWindowShowScenario>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<LoseLevelUIScenario>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindUIViews()
        {
            Container.RegisterInstance(_coreGameSceneUiView).AsSelf();
            Container.RegisterInstance(_buffSelectWindowView).AsSelf();
            Container.RegisterInstance(_winWindowView).AsSelf();
            Container.RegisterInstance(_loseWindowView).AsSelf();
            Container.RegisterInstance(_debugWindowView).AsSelf();
        }
    }
}