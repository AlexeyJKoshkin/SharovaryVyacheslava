namespace Core.Launcher
{
    public interface IStateInfrastructure
    {
        ISceneLoader SceneLoader { get; }
        Contexts Contexts { get; }
    }

  

    public class StateInfrastructure : IStateInfrastructure
    {
        public StateInfrastructure(Contexts context, ISceneLoader sceneLoader)
        {
            Contexts = context;
            SceneLoader = sceneLoader;
        }

        public ISceneLoader SceneLoader { get; }
        public Contexts Contexts { get; }
    }
}
