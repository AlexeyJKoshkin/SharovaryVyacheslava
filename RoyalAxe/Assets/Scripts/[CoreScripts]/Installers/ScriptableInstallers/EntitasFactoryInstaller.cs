using RoyalAxe.CharacterStat;
using RoyalAxe.CoreLevel;
using RoyalAxe.EntitasSystems;
using RoyalAxe.GameEntitas;
using UnityEngine;
using VContainer;

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/EntitasFactoryInstaller", fileName = "EntitasFactoryInstaller")]
    public class EntitasFactoryInstaller : ScriptableInstaller
    {
        protected override void InstallBindings()
        {
            Container.Register<UnitsEntityFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            BindViewBuilder();

            BindBluePrints();

            BindItemWrapperHelpers();
            
            Container.Register<SkillFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<BosonUnitPipeline>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UnitsInfluenceCalculator>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UnitDamageApplierFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindItemWrapperHelpers()
        {
            Container.Register<UnitItemFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindBluePrints()
        {
            SingletonAllInterfaces<BluePrintsFactoryStorage>();
            SingletonAllInterfaces<UnitsBlueprintFactory>();
            SingletonAllInterfaces<SkillBluePrintFactory>();
          
        }

        private void BindViewBuilder()
        {
            Container.Register<UnitsViewBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UnitsBuilderFacade>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}