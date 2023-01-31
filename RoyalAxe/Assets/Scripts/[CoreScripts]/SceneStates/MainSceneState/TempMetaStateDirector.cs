using Core.UserProfile;
using ProjectUI;
using RoyalAxe.CoreLevel;
using VContainer.Unity;

namespace Core.Launcher
{
    public class TempMetaStateDirector
    {
        private readonly MetaSceneUIView _metaSceneUIView;
        private readonly StateLoaderProvider _stateLoaderProvider;
        private readonly IUltimateCheatAdapter _ultimateCheatAdapter;
        private readonly IUserSaveProfileStorage _userSaveProfileStorage;

        public TempMetaStateDirector(MetaSceneUIView metaSceneUIView,
                                     StateLoaderProvider sceneLoaderProvider,
                                     IUltimateCheatAdapter ultimateCheatAdapter,
                                     IUserSaveProfileStorage userSaveProfileStorage)
        {
            _metaSceneUIView = metaSceneUIView;
            _stateLoaderProvider = sceneLoaderProvider;
            _ultimateCheatAdapter = ultimateCheatAdapter;
            _userSaveProfileStorage = userSaveProfileStorage;
        }

        public void Initialize()
        {
            _metaSceneUIView.TempView.OnClickStartGameBtn += OnStartGameHandler;
        }

        void OnStartGameHandler()
        {
            var coreLevelParams = GetLevelParams();
            _stateLoaderProvider.Set<CoreGameSceneLoader>().SetPlayerParameters(coreLevelParams);
        }

        private LastLevel GetLevelParams()
        {
          
            var current = _userSaveProfileStorage.Current;
            return current.LevelProgressFacade.SavedLevel;
        }
    }
}