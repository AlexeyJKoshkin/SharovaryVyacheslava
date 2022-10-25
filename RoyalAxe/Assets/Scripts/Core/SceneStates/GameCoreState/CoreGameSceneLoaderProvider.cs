namespace Core.Launcher
{
    public interface ICoreGameHandlerAdapter
    {
        void LoadMetaScene();
    }

    public class CoreGameSceneLoaderProvider : IStateLoaderProvider, ICoreGameHandlerAdapter
    {
        private ISceneLoaderHelper _currentLoader;

        public ISceneLoaderHelper GetCurrentSceneLoader()
        {
            return _currentLoader;
        }

        public void Initialize() { }

        public void LoadMetaScene()
        {
            _currentLoader = new MockSceneLoader(GameSceneType.Meta);
        }

        public void RestartLevel() { }
    }
}
