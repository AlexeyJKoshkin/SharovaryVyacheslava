using System.Collections.Generic;

namespace Core.UserProfile
{
    class HeroProgressDefaultFactory :  IDefaultProgressFactory<UserAllHeroesProgress>
    {
        public UserAllHeroesProgress CreateDefault()
        {
            var current = new HeroProgressData()
            {
               CharacterRecord = new SaveEntityRecord(){Id = "Default_Hero", Level = 1},
               EquipWeapon = "Weapon_Player_default"
            };

            return new UserAllHeroesProgress()
            {
                SavedHeroes = new List<HeroProgressData>(){current},
                SelectedHeroId = current.CharacterRecord.Id
            };
        }
    }
}
