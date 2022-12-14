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
        AbstractBuffStrategy<T>.StrategySettings GetSettings<T>() where T : BaseLevelBuffSettings;
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
        public AbstractBuffStrategy<T>.StrategySettings GetSettings<T>(Func<LevelBuffSettingsComposite, T> settingGetter) where T : BaseLevelBuffSettings
        {
            var settings = settingGetter?.Invoke(SettingsComposite);
            if(settings== null) return new AbstractBuffStrategy<T>.StrategySettings();

            var additionalSettings = _helper[settings.Type];
            
            var result = new AbstractBuffStrategy<T>.StrategySettings()
            {
                Settings = settings,
                Type = settings.Type,
                IsSingle = additionalSettings != null && additionalSettings.IsSingle
            };
            return result;
        }
        
        public AbstractBuffStrategy<T>.StrategySettings GetSettings<T>() where T : BaseLevelBuffSettings
        {
            return GetSettings<T>((composite) => { return composite.AllSettings().First(o => o is T) as T; });
        }

        class Helper : Dictionary<LevelBuffType,LevelAdditionSettingsAttribute>
        {
            public Helper()
            {
                
                var enumType = typeof(LevelBuffType);
                foreach (LevelBuffType memberName in enumType.GetEnumValues())
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