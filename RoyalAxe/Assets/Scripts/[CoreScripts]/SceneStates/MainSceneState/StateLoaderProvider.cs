namespace Core.Launcher 
{
    public interface IStateLoaderProvider
    {
        ISceneLoaderHelper GetCurrentSceneLoader();
    }
    
    public class StateLoaderProvider : IStateLoaderProvider
    {
        private ISceneLoaderHelper _currentSceneLoader;
        private readonly ISceneLoaderProvider _sceneLoaderProvider;
        public StateLoaderProvider(ISceneLoaderProvider sceneLoaderProvider)
        {
            _sceneLoaderProvider = sceneLoaderProvider;
        }

        public ISceneLoaderHelper GetCurrentSceneLoader()
        {
            return _currentSceneLoader;
        }

        public T Set<T>() where T : ISceneLoaderHelper
        {
            _currentSceneLoader = _sceneLoaderProvider.GetLoader<T>();
            return (T) _currentSceneLoader;
        }
    }
}