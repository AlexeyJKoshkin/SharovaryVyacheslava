using Core.Launcher;
using Core.UserProfile;
using RoyalAxe;
using RoyalAxe.CoreLevel;
using RoyalAxe.Map;
using UnityEngine;
using VContainer;

namespace Core 
{
    [CreateAssetMenu(menuName = "Installers/CoreLevelGenerationInstaller", fileName = "CoreLevelGenerationInstaller")]
    public class CoreLevelGenerationInstaller : ScriptableInstaller
    {
        [SerializeField]
        private TileCoreMapSettings _coreMapSettings;

        [SerializeField] private CoreGamePlayPrefabStorage _prefabStorage;
        
        protected override void InstallBindings()
        {
            Container.RegisterInstance(_prefabStorage);

            Container.Register<CoreGameSceneLoaderProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            InstallMap();
            InstallWizardShop();

            InstallNodeLogic();
        }

        private void InstallNodeLogic()
        {
            Container.Register<ShowWinWindowNode>(Lifetime.Singleton).AsSelf();
            Container.Register<SpawnMobNode>(Lifetime.Singleton).AsSelf();
            Container.Register<LoadNextWaveNode>(Lifetime.Singleton).AsSelf();
            Container.Register<CoreGameBehaviourNode>(Lifetime.Singleton).AsSelf();
        }

        private void InstallMap()
        {
            Container.Register<LevelCreationOperation>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<PlayerCoreGameFacade>(Lifetime.Singleton).AsImplementedInterfaces();

            Container.Register<MockLevelCoreMap>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<MobPositionGenerator>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<LineRoyalAxeMapBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            
            Container.Register<LevelAdapter>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<LevelPositionCalculation>(Lifetime.Singleton).AsImplementedInterfaces();
            
            Container.RegisterInstance(_coreMapSettings);
            
            Container.Register<MobSpawnOperation>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<MobSpawnFacade>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<WaveLevelSwitcher>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<MobBlueprintsForSpawnStorage>(Lifetime.Singleton).AsImplementedInterfaces();

        }

        private void InstallWizardShop()
        {
        }
    }
}