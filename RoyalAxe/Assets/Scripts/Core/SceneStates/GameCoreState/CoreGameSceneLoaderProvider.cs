using Core.Installers;
using RoyalAxe.CoreLevel;

namespace Core.Launcher
{
    public interface ICoreGameHandlerAdapter
    {
       void LoadMetaScene();
    }

    public class CoreGameSceneLoaderProvider : IStateLoaderProvider, ICoreGameHandlerAdapter
    {
        private ISceneLoaderHelper _currentLoader;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSwitcher;
        public CoreGameSceneLoaderProvider(IRoyalAxePauseSystemSwitcher pauseSwitcher)
        {
            _pauseSwitcher = pauseSwitcher;
        }

        public ISceneLoaderHelper GetCurrentSceneLoader()
        {
            return _currentLoader;
        }

        public void Initialize() { }

        public void LoadMetaScene()
        {
            _pauseSwitcher.UnPause();
            _currentLoader = new MockSceneLoader(GameSceneType.Meta);
        }


    }
}
