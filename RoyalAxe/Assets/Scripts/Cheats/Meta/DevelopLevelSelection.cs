using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.CoreLevel;
using VContainer.Unity;

namespace RoyalAxe 
{
    public class DevelopLevelSelection : IDevelopSelectCoreParamsWorker
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

         public void PrepareViews()
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
                 _storage.TryGetValue(levelNumber.ToString(), out levelData);

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