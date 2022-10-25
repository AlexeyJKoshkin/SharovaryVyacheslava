using RoyalAxe.CoreLevel;

namespace Core.Launcher
{
    public interface ICoreGameInfrastructure : IStateInfrastructure
    {
        ILevelCreation LevelCreation { get;  }
        IStateLoaderProvider StateLoaderProvider { get; }
    }
    
    public class CoreGameSceneInfrastructure : StateInfrastructure,ICoreGameInfrastructure
    {
        public CoreGameSceneInfrastructure(Contexts context,ISceneLoader sceneLoader, ILevelCreation levelCreation, IStateLoaderProvider stateLoaderProvider) : base(context, sceneLoader)
        {
            LevelCreation = levelCreation;
            StateLoaderProvider = stateLoaderProvider;
        }

        public ILevelCreation LevelCreation { get;  }
        public IStateLoaderProvider StateLoaderProvider { get; }
    }
}
