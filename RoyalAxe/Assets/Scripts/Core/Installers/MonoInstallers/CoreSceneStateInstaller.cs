using RoyalAxe;
using RoyalAxe.CoreLevel;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Core {
    public class CoreSceneStateInstaller : MonoInstaller
    {
        [SerializeField] private LevelInfrastructureView _levelInfrastructureView;
        [FoldoutGroup("UI")]
        [SerializeField] private CoreGameSceneUIView _coreGameSceneUiView;
        [FoldoutGroup("UI")]
        [SerializeField] private BuffSelectWindowView _buffSelectWindowView;
        protected override void InstallBindings()
        {
            Container.RegisterInstance(_levelInfrastructureView).AsSelf();
            Container.RegisterInstance(_coreGameSceneUiView).AsSelf();
        }
    }
}