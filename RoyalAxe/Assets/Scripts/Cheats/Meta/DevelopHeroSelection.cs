using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using Core.UserProfile;
using GameKit;
using RoyalAxe.Configs;
using RoyalAxe.CoreLevel;
using RoyalAxe.GameEntitas;

namespace RoyalAxe 
{
    public class DevelopHeroSelection : IDevelopSelectCoreParamsWorker
    {
        private readonly IDataStorage _storage;
        private readonly DevelopSelectPreCoreParamsView _view;
        private readonly IUltimateCheatAdapter _cheatAdapter;


        public DevelopHeroSelection(IDataStorage storage, IUltimateCheatAdapter cheatAdapter, DevelopSelectLevelParamsUIView developView)
        {
            _storage      = storage;
            _cheatAdapter = cheatAdapter;
            _view         = developView.SelectHeroParamsView;
        }

        public void PrepareViews()
        {
            var heroes = new List<string>() {"Select Hero"};
            _storage.All<HeroUnitJsonData>()
                                 .Select(o => o.UniqueID)
                                 .Where(o => o.Contains(UnitsEntityExtension.HERO_POSTFIX))
                                 .ForEach(heroes.Add);
            
            _view.OnChangeSelectionItemParamsEvent += OnChangeItemParamsSelectionHandler;

            HeroProgressData heroProgressData = null;
            
            if (_cheatAdapter.Cheats.hasHeroStartLevel)
            {
                heroProgressData = _cheatAdapter.Cheats.heroStartLevel;
                OnChangeItemParamsSelectionHandler(heroProgressData.Id, heroProgressData.Level);
            }
            else
            {
                heroProgressData = new HeroProgressData();
            }
           
            _view.SetItemData(heroes, heroProgressData);

        }

        private void OnChangeItemParamsSelectionHandler(string heroId, int heroLvl)
        {
            if (_storage.Contains<HeroUnitJsonData>(heroId))
            {
                _cheatAdapter.Cheats.ReplaceHeroStartLevel(heroId, heroLvl);
            }
            else
                _cheatAdapter.Cheats.RemoveHeroStartLevel();
        }
      
    }
}