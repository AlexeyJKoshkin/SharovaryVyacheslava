using System;
using Core;

namespace RoyalAxe.LevelBuff
{
    public class FiringBladeBuff : AbstractBuffStrategy<FiringBladeBuffSettings>
    {
        




        public override void DoBuffStrategyActivate()
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

        public FiringBladeBuff(ILevelBuffSettingCompositeProvider provider) : base(provider) { }
    }
}