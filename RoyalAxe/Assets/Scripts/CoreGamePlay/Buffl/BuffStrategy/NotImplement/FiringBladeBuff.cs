using System;
using Core;

namespace RoyalAxe.LevelBuff
{
    public class FiringBladePower : AbstractPowerStrategyStrategy<FiringBladeBuffSettings>
    {
        




        public override void DoLevelPowerActivate()
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

        public override void DoLevelPowerDeActivate()
        {
            
        }

        public FiringBladePower(ILevelBuffSettingCompositeProvider provider) : base(provider) { }
    }
}