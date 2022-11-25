using RoyalAxe.CharacterStat;
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
           
            BindSingletonWithAllInterfaces<UnitAddDamageUtility>();
            Container.Register<SkillFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<BosonUnitPipeline>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UnitsInfluenceCalculator>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UnitDamageApplierFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void BindViewBuilder()
        {
            Container.Register<UnitsViewBuilder>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<UnitsBuilderFacade>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}