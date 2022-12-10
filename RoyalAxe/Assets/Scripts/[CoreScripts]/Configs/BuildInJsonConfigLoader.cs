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
            if (typeof(T) == typeof(StatCollection)) return _buildInJsonDataProvider.UnitStatText;
            if (typeof(T) == typeof(LevelSettingsData)) return _buildInJsonDataProvider.LevelDataText;
            if (typeof(T) == typeof(WeaponsSkillConfigDef)) return _buildInJsonDataProvider.WeaponSkillText;
            if (typeof(T) == typeof(WizardLevelCollection)) return _buildInJsonDataProvider.WizardLevelText;
            if (typeof(T) == typeof(LevelBuffSettingsComposite)) return _buildInJsonDataProvider.LevelBufSettings;

            HLogger.LogError($"Not Found json Asset {typeof(T).Name}");
            return null;
        }
    }
}
