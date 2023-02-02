using Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RoyalAxe 
{
    public class DevelopMetaCheatsInstaller : MonoInstaller
    {
        [SerializeField]
        private DevelopSelectLevelParamsUIView _developSelectLevelParamsUiView;
        protected override void InstallBindings()
        {

            Container.Register<DevelopHeroSelection>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<DevelopHeroWeaponSelection>(Lifetime.Singleton).AsImplementedInterfaces();
            Container.Register<DevelopLevelSelection>(Lifetime.Singleton).AsImplementedInterfaces();
        
            Container.RegisterInstance(_developSelectLevelParamsUiView);
            Container.RegisterEntryPoint<DevelopSelectParamsEntryPoint>();
        }
    }
}