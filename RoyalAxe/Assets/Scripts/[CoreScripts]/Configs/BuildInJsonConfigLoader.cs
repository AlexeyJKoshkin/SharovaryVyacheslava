using Core;
using Core.Configs;
using RoyalAxe.Units.Stats;
using RoyalAxe.CoreLevel;
using RoyalAxe.LevelSkill;

namespace RoyalAxe.Configs
{
    public class BuildInJsonConfigLoader : IJsonConfigFileLoader
    {
        private readonly BuildInJsonDataProvider _buildInJsonDataProvider;
        public BuildInJsonConfigLoader(BuildInJsonDataProvider buildInJsonDataProvider)
        {
            _buildInJsonDataProvider = buildInJsonDataProvider;
        }

        public string LoadText<T>(string path) where T : class
        {
            if (typeof(T) == typeof(LevelSettingsData)) return _buildInJsonDataProvider.LevelDataText;
            if (typeof(T) == typeof(UnitWeaponSkillConfigDef)) return _buildInJsonDataProvider.HeroWeaponSkillText;
            if (typeof(T) == typeof(WizardLevelCollection)) return _buildInJsonDataProvider.WizardLevelText;
            if (typeof(T) == typeof(LevelBuffSettingsComposite)) return _buildInJsonDataProvider.LevelBufSettings;
            if (typeof(T) == typeof(MobUnitJsonData)) return _buildInJsonDataProvider.MobUnitSettings;
            if (typeof(T) == typeof(HeroUnitJsonData)) return _buildInJsonDataProvider.HeroUnitSettings;

            HLogger.LogError($"Not Found json Asset {typeof(T).Name}");
            return null;
        }
    }
}
