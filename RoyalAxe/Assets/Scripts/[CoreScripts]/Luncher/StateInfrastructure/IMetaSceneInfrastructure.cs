namespace Core.Launcher
{
    public interface IMetaSceneInfrastructure : IStateInfrastructure
    {
        
        
    }
    
    public class MetaSceneInfrastructure : StateInfrastructure,IMetaSceneInfrastructure
    {
        public MetaSceneInfrastructure(Contexts context,ISceneLoader sceneLoader, IStateLoaderProvider stateLoaderProvider) : base(context, sceneLoader,stateLoaderProvider)
        {
        }
    }
}
