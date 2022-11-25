using RoyalAxe.CoreLevel;

namespace Core.Launcher
{
    public interface ICoreGameInfrastructure : IStateInfrastructure
    {
        ILevelCreation LevelCreation { get;  }
        IStateLoaderProvider StateLoaderProvider { get; }
        ICoreGameUtility LevelUtility { get; }
    }
    
    public class CoreGameSceneInfrastructure : StateInfrastructure,ICoreGameInfrastructure
    {
        public CoreGameSceneInfrastructure(Contexts context,ISceneLoader sceneLoader, ILevelCreation levelCreation, IStateLoaderProvider stateLoaderProvider,
                                           ICoreGameUtility levelUtility) : base(context, sceneLoader)
        {
            LevelCreation = levelCreation;
            StateLoaderProvider = stateLoaderProvider;
            LevelUtility = levelUtility;
        }

        public ILevelCreation LevelCreation { get;  }
        public IStateLoaderProvider StateLoaderProvider { get; }
        public ICoreGameUtility LevelUtility { get; }
    }
}
