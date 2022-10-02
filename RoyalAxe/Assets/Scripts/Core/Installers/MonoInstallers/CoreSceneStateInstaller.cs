using RoyalAxe;
using RoyalAxe.CoreLevel;
using UnityEngine;
using VContainer;

namespace Core {
    public class CoreSceneStateInstaller : MonoInstaller
    {
        [SerializeField] private LevelInfrastructureView _levelInfrastructureView;
        [SerializeField] private CoreGameSceneUIView _coreGameSceneUiView;
        protected override void InstallBindings()
        {
            Container.RegisterInstance(_levelInfrastructureView).AsSelf();
            Container.RegisterInstance(_coreGameSceneUiView).AsSelf();
        }
    }
}