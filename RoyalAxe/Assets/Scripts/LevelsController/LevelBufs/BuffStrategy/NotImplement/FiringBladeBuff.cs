using System;
using Core;

namespace RoyalAxe.LevelBuff
{
    public class FiringBladeBuff : AbstractBuffStrategy
    {
        public override bool IsSingle => true;
        private readonly FiringBladeBuffSettings _settings;

        public FiringBladeBuff(ILevelBuffSettingCompositeProvider provider)
        {
            _settings = provider.SettingsComposite.FiringBladeBuffSetting;
        }


        public override void Activate()
        {
            /*
             * располагается справа от персонажа. Наносит урон по ближним врагам каждое Х время. 
                Параметры:
                Наносимый урон
                Кулдаун нанесения урона
             */

            //todo: надо писать код :-) парящие  щиты
            HLogger.LogError("Парящие щиты");
        }
    }
}