using Core;

namespace RoyalAxe.LevelSkill
{
    public class FiringBladePlayerSkill : AbstractPlayerSkillStrategy<FiringBladeSkillSettings>
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

        public FiringBladePlayerSkill(ILevelBuffSettingCompositeProvider provider) : base(provider) { }
    }
}