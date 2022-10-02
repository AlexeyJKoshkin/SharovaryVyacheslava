using System.Collections.Generic;
using Core;
using GameKit;
using GameKit.Editor;
using UnityEngine;
using VContainer;

namespace ProjectEditorEcoSystem
{
    public class EcoSystemSettingsInstaller : BaseGameEditorInstaller
    {
        [SerializeField] private List<ScriptableInstaller> _installers = new List<ScriptableInstaller>();

        public override void Install(IContainerBuilder diContainer)
        {
            _installers.ForEach(e => e.Install(diContainer));
            ReflectionHelper.GetAllTypesInSolutionWithInterface<IProjectEditorEcosystemGUI>()
                            .ForEach(e => { diContainer.Register(e, Lifetime.Singleton).AsImplementedInterfaces(); });
        }
    }
}