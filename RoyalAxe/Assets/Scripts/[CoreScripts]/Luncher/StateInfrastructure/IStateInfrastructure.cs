namespace Core.Launcher
{
    public interface IStateInfrastructure
    {
        IStateLoaderProvider StateLoaderProvider { get; }
        ISceneLoader SceneLoader { get; }
        Contexts Contexts { get; }
    }

  

    public class StateInfrastructure : IStateInfrastructure
    {
        public StateInfrastructure(Contexts context, ISceneLoader sceneLoader, IStateLoaderProvider stateLoaderProvider)
        {
            Contexts = context;
            SceneLoader = sceneLoader;
            StateLoaderProvider = stateLoaderProvider;
        }

        public IStateLoaderProvider StateLoaderProvider { get; }
        public ISceneLoader SceneLoader { get; }
        public Contexts Contexts { get; }
    }
}
