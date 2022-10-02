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
        protected override void InstallBindings()
        {
            Container.Register<LevelCreationOperation>(Lifetime.Singleton).AsImplementedInterfaces();

            Container.Register<MockLevelCoreMap>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<MobPositionGenerator>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<LineRoyalAxeMapBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            
            Container.Register<LevelAdapter>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.RegisterInstance(_coreMapSettings);
            
            Container.Register<LevelDirector>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<LevelWaveProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}