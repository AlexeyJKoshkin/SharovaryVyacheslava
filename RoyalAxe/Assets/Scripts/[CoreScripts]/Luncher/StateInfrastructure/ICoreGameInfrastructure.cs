using RoyalAxe.CoreLevel;

namespace Core.Launcher
{
    public interface ICoreGameInfrastructure : IStateInfrastructure
    {
        ILevelCreation LevelCreation { get;  }

        ICoreGameUtility LevelUtility { get; }
    }
    
    public class CoreGameSceneInfrastructure : StateInfrastructure,ICoreGameInfrastructure
    {
        public CoreGameSceneInfrastructure(Contexts context,
                                           ISceneLoader sceneLoader,
                                           ILevelCreation levelCreation,
                                           IStateLoaderProvider stateLoaderProvider,
                                           ICoreGameUtility levelUtility
                                          ) : base(context, sceneLoader,stateLoaderProvider)
        {
            LevelCreation = levelCreation;
            LevelUtility = levelUtility;
        }

    public ILevelCreation LevelCreation { get;  }
        public ICoreGameUtility LevelUtility { get; }
    }
}
