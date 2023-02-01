using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.CoreLevel;
using VContainer.Unity;

namespace RoyalAxe
{
    public class DevelopSelectLevelParamsUIView : UIViewComponent
    {
        public DevelopSelectLevelView SelectLevelView;
    }

    public class DevelopSelectParamsEntryPoint : IInitializable
    {
        private readonly IDataStorage _storage;
        private readonly IUltimateCheatAdapter _ultimateCheatAdapter;
        private readonly DevelopSelectLevelParamsUIView _view;

        public DevelopSelectParamsEntryPoint(IDataStorage storage,
                                             IUltimateCheatAdapter ultimateCheatAdapter,
                                             DevelopSelectLevelParamsUIView developSelectLevelParamsUiView)
        {
            _storage              = storage;
            _ultimateCheatAdapter = ultimateCheatAdapter;
            _view                 = developSelectLevelParamsUiView;
        }

        public void Initialize()
        {
            if (_ultimateCheatAdapter.EnableCheats)
            {
                new DevelopLevelSelection(_storage, _ultimateCheatAdapter, _view).Initialize();
            }
            else
            {
                _view.Close();
            }
        }
    }


    public class DevelopLevelSelection : IInitializable
    {
        private readonly IDataStorage _storage;
        private readonly DevelopSelectLevelView _view;
        private readonly IUltimateCheatAdapter _cheatAdapter;

        public DevelopLevelSelection(IDataStorage storage, IUltimateCheatAdapter cheatAdapter, DevelopSelectLevelParamsUIView developView)
        {
            _storage      = storage;
            _cheatAdapter = cheatAdapter;
            _view         = developView.SelectLevelView;
        }

        public void Initialize()
        {
            _view.OnChangeLevelEvent += ViewOnOnChangeLevelEvent;
            if (_cheatAdapter.Cheats.hasCheatStartLevel)
            {
                ViewOnOnChangeLevelEvent(_cheatAdapter.Cheats.cheatStartLevel.Level.LevelNumber);
            }
            else  _view.DrawLevel(null);

        }


        private void ViewOnOnChangeLevelEvent(int levelNumber)
        {
            LevelSettingsData levelData = null;
            if (levelNumber > 0)
                levelData = _storage.ById<LevelSettingsData>(levelNumber.ToString());

            _view.DrawLevel(levelData);
            if (levelData == null)
            {
                _cheatAdapter.Cheats.RemoveCheatStartLevel();
            }
            else
            {
                _cheatAdapter.Cheats.ReplaceCheatStartLevel(new LastLevel() {Biome = levelData.Type, LevelNumber = levelNumber});
            }
        }
    }
}