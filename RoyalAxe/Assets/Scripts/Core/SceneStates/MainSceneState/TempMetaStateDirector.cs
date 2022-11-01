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
        private readonly IUltimateCheatAdapter _ultimateCheatAdapter;

        private ISceneLoaderHelper _currentSceneLoader;

        public TempMetaStateDirector(MetaSceneUIView metaSceneUIView, ISceneLoaderProvider sceneLoaderProvider, IUltimateCheatAdapter ultimateCheatAdapter)
        {
            _metaSceneUIView = metaSceneUIView;
            _sceneLoaderProvider = sceneLoaderProvider;
            _ultimateCheatAdapter = ultimateCheatAdapter;
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
            if (_ultimateCheatAdapter.UseLevelFromCheat) return _ultimateCheatAdapter.LevelParams;
            return new CoreLevelParameters()
            {
                BiomeType = BiomeType.Desert, // по хорошему либо выбирать из меню либо грузить из прогресса
                StartLevel = 1
            };
        }

        public ISceneLoaderHelper GetCurrentSceneLoader()
        {
            return _currentSceneLoader;
        }
    }
}
