namespace Core.Launcher
{
    public interface IMetaSceneInfrastructure : IStateInfrastructure
    {
        IStateLoaderProvider StateLoaderProvider { get; }
    }
    
    public class MetaSceneInfrastructure : StateInfrastructure,IMetaSceneInfrastructure
    {
        public MetaSceneInfrastructure(Contexts context,ISceneLoader sceneLoader, IStateLoaderProvider stateLoaderProvider) : base(context, sceneLoader)
        {
            StateLoaderProvider = stateLoaderProvider;
        }
        public IStateLoaderProvider StateLoaderProvider { get; }
    }
}
