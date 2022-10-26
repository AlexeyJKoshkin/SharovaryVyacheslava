using Core.Installers;
using RoyalAxe.CoreLevel;

namespace Core.Launcher
{
    public interface ICoreGameHandlerAdapter
    {
        void LoadMetaScene();
        void RestartLevel();
    }

    public class CoreGameSceneLoaderProvider : IStateLoaderProvider, ICoreGameHandlerAdapter
    {
        private ISceneLoaderHelper _currentLoader;
        private readonly IRoyalAxePauseSystemSwitcher _pauseSwitcher;
        private readonly ISceneLoaderProvider _sceneLoaderProvider;
        public CoreGameSceneLoaderProvider(IRoyalAxePauseSystemSwitcher pauseSwitcher, ISceneLoaderProvider sceneLoaderProvider)
        {
            _pauseSwitcher = pauseSwitcher;
            _sceneLoaderProvider = sceneLoaderProvider;
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

        public void RestartLevel()
        {
            var coreLevelParams =  new CoreLevelParameters()
            {
                BiomeType = BiomeType.Desert, // по хорошему либо выбирать из меню либо грузить из прогресса
                StartLevel = 4 
            };
            
            var currentSceneLoader = _sceneLoaderProvider.GetLoader<CoreGameSceneLoader>();
            currentSceneLoader.SetPlayerParameters(coreLevelParams);
            _currentLoader = currentSceneLoader;
        }
    }
}
