using Core.Configs;

namespace RoyalAxe.LevelBuff 
{
    public interface ILevelBuffSettingCompositeProvider
    {
        LevelBuffSettingsComposite SettingsComposite { get; }
    }

    public class LevelBuffSettingCompositeProvider : ILevelBuffSettingCompositeProvider 
    { 
        public LevelBuffSettingCompositeProvider(IJsonConfigsModelsLoader configsModelsLoader)
        {
            SettingsComposite = configsModelsLoader.LoadSingle<LevelBuffSettingsComposite>() ?? new LevelBuffSettingsComposite();
        }

        public LevelBuffSettingsComposite SettingsComposite { get; private set; }
    }
}