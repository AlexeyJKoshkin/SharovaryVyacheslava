using System.Linq;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelSkill
{
    public class IncreaseDamagePlayerSkill : AbstractPlayerSkillStrategy<IncreaseDamageSkillSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;

        public IncreaseDamagePlayerSkill(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider) : base(provider)
        {
            _unitsContext             = unitsContext;
        }

        public override void DoLevelPowerActivate()
        {
            //либо увеличиваем физ дамаг как стат
            // player.physicalDamage.ChangeValue(_increaseDamage).ApplyPermanentMod();
            //либо увеличиваем физ дамаг в способности
          
            var damageComponent = Player.mainDamage;
            damageComponent.IncreaseDamage(DamageType.Physical, Settings.Value);

        }

        public override void DoLevelPowerDeActivate()
        {
            var damageComponent = Player.mainDamage;
            damageComponent.IncreaseDamage(DamageType.Physical, -Settings.Value);
        }
    }
}