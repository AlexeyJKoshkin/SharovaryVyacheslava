using System.Linq;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    public class IncreaseDamageBuff : AbstractBuffStrategy<IncreaseDamageBuffSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;

        public IncreaseDamageBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider) : base(provider)
        {
            _unitsContext             = unitsContext;
        }

        public override void DoBuffStrategyActivate()
        {
            //либо увеличиваем физ дамаг как стат
            // player.physicalDamage.ChangeValue(_increaseDamage).ApplyPermanentMod();
            //либо увеличиваем физ дамаг в способности
          
            var damageComponent = Player.mainDamage;
            damageComponent.IncreaseDamage(DamageType.Physical, Settings.Value);

        }
    }
}