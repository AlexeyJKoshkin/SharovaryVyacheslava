using ProjectUI;
using RoyalAxe.CoreLevel;
namespace Core.Launcher
{
    public interface IStateLoaderProvider
    {
        ISceneLoaderHelper GetCurrentSceneLoader();
        void Initialize();
    }

    public class TempMetaStateDirector : IStateLoaderProvider
    {
        private readonly MetaSceneUIView _metaSceneUIView;
        private readonly ISceneLoaderProvider _sceneLoaderProvider;

        private ISceneLoaderHelper _currentSceneLoader;

        public TempMetaStateDirector(MetaSceneUIView metaSceneUIView, ISceneLoaderProvider sceneLoaderProvider)
        {
            _metaSceneUIView = metaSceneUIView;
            _sceneLoaderProvider = sceneLoaderProvider;
        }

        public void Initialize()
        {
            _metaSceneUIView.TempView.OnClickStartGameBtn += OnStartGameHandler;
        }

        void OnStartGameHandler()
        {
            var coreLevelParams = GetLevelParams();
             var currentSceneLoader = _sceneLoaderProvider.GetLoader<CoreGameSceneLoader>();
            currentSceneLoader.SetPlayerParameters(coreLevelParams);
            _currentSceneLoader = currentSceneLoader;
        }

        private CoreLevelParameters GetLevelParams()
        {
            return new CoreLevelParameters()
            {
                BiomeType = BiomeType.Desert, // по хорошему либо выбирать из меню либо грузить из прогресса
                StartLevel = 4 
            };
        }

        public ISceneLoaderHelper GetCurrentSceneLoader()
        {
            return _currentSceneLoader;
        }
    }
}
