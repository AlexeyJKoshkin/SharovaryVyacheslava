using Core.UserProfile;
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
        private GameRootLoopContext _gameRootLoopContext;

        private ISceneLoaderHelper _currentSceneLoader;

        public TempMetaStateDirector(MetaSceneUIView metaSceneUIView, ISceneLoaderProvider sceneLoaderProvider, IUltimateCheatAdapter ultimateCheatAdapter, GameRootLoopContext gameRootLoopContext)
        {
            _metaSceneUIView = metaSceneUIView;
            _sceneLoaderProvider = sceneLoaderProvider;
            _ultimateCheatAdapter = ultimateCheatAdapter;
            _gameRootLoopContext = gameRootLoopContext;
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

        private LastLevel GetLevelParams()
        {
            if (_ultimateCheatAdapter.UseLevelFromCheat) return _ultimateCheatAdapter.LevelParams;
            return _gameRootLoopContext.userProgressEntity.UserCurrentLevelsProgress.Progress;
        }

        public ISceneLoaderHelper GetCurrentSceneLoader()
        {
            return _currentSceneLoader;
        }
    }
}
