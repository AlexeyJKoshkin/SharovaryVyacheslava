namespace Core.Launcher
{
    public interface IStateInfrastructure
    {
        ISceneStateSettings StateSettings { get; }
        ISceneLoader SceneLoader { get; }
        GameRootLoopContext Context { get; }
        IRoyalAxeFeatureBuilder SystemsBuilder { get; }
    }

    public class StateInfrastructure : IStateInfrastructure
    {
        public StateInfrastructure(GameRootLoopContext context, IRoyalAxeFeatureBuilder systemsBuilder, ISceneStateSettings stateSettings, ISceneLoader sceneLoader)
        {
            Context        = context;
            SystemsBuilder = systemsBuilder;
            StateSettings  = stateSettings;
            SceneLoader    = sceneLoader;
        }

        public ISceneStateSettings StateSettings { get; }
        public ISceneLoader SceneLoader { get; }
        public GameRootLoopContext Context { get; }
        public IRoyalAxeFeatureBuilder SystemsBuilder { get; }
    }
}