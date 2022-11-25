using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(menuName = "Installers/GameLoopInstaller", fileName = "GameLoopInstaller")]
    public class GameLoopScriptableInstaller : ScriptableInstaller
    {
        private void BindStates()
        {
            HashSet<Type> types = new HashSet<Type>();
            /*StateMachineTypesProvider.AllStateMachineType().ForEach(e => types.Add(e));
            StateMachineTypesProvider.AllStatesMachineType().ForEach(e => types.Add(e));
            types.ForEach(e => Container.BindInterfacesAndSelfTo(e).AsSingle());*/
        }

        protected override void InstallBindings()
        {
            throw new NotImplementedException();
        }
    }
}