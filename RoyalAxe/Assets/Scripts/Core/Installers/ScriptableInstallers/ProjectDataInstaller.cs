using Core.Data.Provider;
using GameKit;
using UnityEngine;
using VContainer;

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/ProjectDataInstaller", fileName = "ProjectDataInstaller")]
    public class ProjectDataInstaller : ScriptableInstaller
    {
        [SerializeField] private ProjectDataStorage _dataStorage;

        protected override void InstallBindings()
        {
            _dataStorage.Reload();
            _dataStorage.Collection.ForEach(e => Container.RegisterInstance(e.GetType()).AsImplementedInterfaces());

            Container.Register<LocalDataBoxStorage>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}