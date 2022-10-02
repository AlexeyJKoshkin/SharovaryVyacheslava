using System.Collections.Generic;

namespace Core.Launcher
{
    public interface IMaineSceneStateProvider
    {
        IEnumerable<IMainSceneState> AllStates();
        T GetState<T>() where T : IMainSceneState;
    }
}