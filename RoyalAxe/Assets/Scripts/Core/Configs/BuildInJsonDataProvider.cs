using System.Collections.Generic;
using UnityEngine;

namespace RoyalAxe.Configs
{
    public class BuildInJsonDataProvider : ScriptableObject
    {
        public string WeaponSkillText => GetText(_weaponSkill);
        public string WizardLevelText => GetText(_wizardShop);
        public string UnitStatText => GetText(_unitStatCollection);
        public string LevelDataText => GetText(_levelsData);
        public string LevelBufSettings => GetText(_levelBuffData);

        [SerializeField] private TextAsset _weaponSkill;
        [SerializeField] private TextAsset _wizardShop;
        
        [SerializeField] private TextAsset _unitStatCollection;

        [SerializeField] private TextAsset _levelsData;
        [SerializeField] private TextAsset _levelBuffData;
        
        private string GetText(TextAsset asset)
        {
            if (asset == null) return null;
            return asset.text;
        }
    }
}
