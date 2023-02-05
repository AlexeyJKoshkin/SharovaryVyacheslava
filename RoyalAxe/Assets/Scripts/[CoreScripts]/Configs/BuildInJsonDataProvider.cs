using UnityEngine;

namespace RoyalAxe.Configs
{
    public class BuildInJsonDataProvider : ScriptableObject
    {
        public string HeroWeaponSkillText => GetText(_heroWeaponSkill);
        public string WizardLevelText => GetText(_wizardShop);
        public string LevelDataText => GetText(_levelsData);
        public string LevelBufSettings => GetText(_levelBuffData);
        public string MobUnitSettings => GetText(_mobUnitData);
        public string HeroUnitSettings => GetText(_heroCharacters);

        [SerializeField] private TextAsset _heroWeaponSkill;
        [SerializeField] private TextAsset _heroCharacters;
        [SerializeField] private TextAsset _wizardShop;

        [SerializeField] private TextAsset _levelsData;
        [SerializeField] private TextAsset _levelBuffData;
        
        [SerializeField] private TextAsset _mobUnitData;
        
        private string GetText(TextAsset asset)
        {
            if (asset == null) return null;
            return asset.text;
        }
    }
}
