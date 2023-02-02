using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using Core.UserProfile;
using GameKit;
using RoyalAxe.CoreLevel;
using RoyalAxe.Units.Stats;

namespace RoyalAxe
{
    public class DevelopHeroWeaponSelection : IDevelopSelectCoreParamsWorker
    {
        private readonly IDataStorage _storage;
        private readonly DevelopSelectPreCoreParamsView _view;
        private readonly IUltimateCheatAdapter _cheatAdapter;


        public DevelopHeroWeaponSelection(IDataStorage storage, IUltimateCheatAdapter cheatAdapter, DevelopSelectLevelParamsUIView developView)
        {
            _storage      = storage;
            _cheatAdapter = cheatAdapter;
            _view         = developView.SelectWeaponCoreParamsView;
        }

        public void PrepareViews()
        {
            var weapons = new List<string>() {"Select Weapon"};
            _storage.All<WeaponsSkillConfigDef>()
                    .Select(o => o.UniqueID)
                    .Where(o => o.Split('_').Length == 4) // тип вещи рарность подтип вещи и айдишник вещи 
                    .ForEach(weapons.Add);


            _view.OnChangeSelectionItemParamsEvent += OnChangeItemParamsSelectionHandler;

            SaveEntityRecord weaponProgressData = null;

            if (_cheatAdapter.Cheats.hasHeroStartWeapon)
            {
                weaponProgressData = _cheatAdapter.Cheats.heroStartWeapon;
                OnChangeItemParamsSelectionHandler(weaponProgressData.Id, weaponProgressData.Level);
            }
            else
            {
                weaponProgressData = new WeaponProgressData();
            }

            _view.SetItemData(weapons, weaponProgressData);
        }

        private void OnChangeItemParamsSelectionHandler(string heroId, int heroLvl)
        {
            if (_storage.Contains<WeaponsSkillConfigDef>(heroId))
            {
                _cheatAdapter.Cheats.ReplaceHeroStartWeapon(heroId, heroLvl);
            }
            else
                _cheatAdapter.Cheats.RemoveHeroStartWeapon();
        }
    }
}