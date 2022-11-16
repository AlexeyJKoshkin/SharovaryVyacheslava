using System;
using System.Collections.Generic;

namespace Core.UserProfile {
    [Serializable]
    public class UserAllHeroesProgress : BaseUserProgressData
    {
        public string SelectedHeroId;
        public List<HeroProgressData> ProgressData = new List<HeroProgressData>();
    }
}