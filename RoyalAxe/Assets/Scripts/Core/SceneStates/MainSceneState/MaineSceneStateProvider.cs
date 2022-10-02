using System;
using System.Collections.Generic;
using GameKit;

namespace Core.Launcher
{
    public class MaineSceneStateProvider : Dictionary<Type, IMainSceneState>, IMaineSceneStateProvider
    {
        public MaineSceneStateProvider(IMainSceneState[] allMainStates)
        {
            allMainStates.ForEach(e => Add(e.GetType(), e));
        }

        public IEnumerable<IMainSceneState> AllStates()
        {
            return Values;
        }

        public T GetState<T>() where T : IMainSceneState
        {
            return TryGetValue(typeof(T), out var result) ? (T) result : default;
        }
    }
}