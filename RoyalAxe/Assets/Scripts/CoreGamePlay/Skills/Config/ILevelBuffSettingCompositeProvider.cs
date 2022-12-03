using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Core.Configs;

namespace RoyalAxe.LevelBuff 
{
    public interface ILevelBuffSettingCompositeProvider
    {
        AbstractPowerStrategyStrategy<T>.StrategySettings GetSettings<T>() where T : BaseLevelSkillSettings;
    }

    public class LevelBuffSettingCompositeProvider : ILevelBuffSettingCompositeProvider
    {
        private Helper _helper;
        public LevelBuffSettingCompositeProvider(IJsonConfigsModelsLoader configsModelsLoader)
        {
            SettingsComposite = configsModelsLoader.LoadSingle<LevelBuffSettingsComposite>() ?? new LevelBuffSettingsComposite();
            _helper = new Helper();
        }

        public LevelBuffSettingsComposite SettingsComposite { get; private set; }
       
        public AbstractPowerStrategyStrategy<T>.StrategySettings GetSettings<T>() where T : BaseLevelSkillSettings
        {
            var settings = SettingsComposite.AllSettings().FirstOrDefault(o => o is T) as T;
            if(settings== null) return new AbstractPowerStrategyStrategy<T>.StrategySettings();

            var additionalSettings = _helper[settings.Type];
            
            var result = new AbstractPowerStrategyStrategy<T>.StrategySettings()
            {
                Settings = settings,
                Type     = settings.Type,
                IsSingle = additionalSettings != null && additionalSettings.IsSingle
            };
            return result;
            

        }

        class Helper : Dictionary<LevelSkillType,LevelAdditionSettingsAttribute>
        {
            public Helper()
            {
                
                var enumType = typeof(LevelSkillType);
                foreach (LevelSkillType memberName in enumType.GetEnumValues())
                {
                    var memberInfos = enumType.GetMember(memberName.ToString());
                    var enumValueMemberInfo = memberInfos.First(m => m.DeclaringType == enumType);
                    Add(memberName, Get(enumValueMemberInfo));
                }
            }

            LevelAdditionSettingsAttribute Get(MemberInfo memberInfo)
            {
               
                var valueAttributes = memberInfo.GetCustomAttributes(typeof(LevelAdditionSettingsAttribute), false);
                if (valueAttributes.Length == 0) return null;
                return valueAttributes[0] as LevelAdditionSettingsAttribute;
                
            }
        }


    }
}