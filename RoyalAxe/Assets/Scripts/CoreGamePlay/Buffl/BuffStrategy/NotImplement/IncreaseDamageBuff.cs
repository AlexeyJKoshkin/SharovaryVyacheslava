using System.Linq;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    public class IncreaseDamageBuff : AbstractBuffStrategy<IncreaseDamageBuffSettings>
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;
        private readonly IUnitDamageApplierFactory _unitDamageApplierFactory;

        public IncreaseDamageBuff(UnitsContext unitsContext, ILevelBuffSettingCompositeProvider provider, IUnitDamageApplierFactory unitDamageApplierFactory) : base(provider)
        {
            _unitsContext             = unitsContext;
            _unitDamageApplierFactory = unitDamageApplierFactory;
        }

        public override void DoBuffStrategyActivate()
        {
            //либо увеличиваем физ дамаг как стат
            // player.physicalDamage.ChangeValue(_increaseDamage).ApplyPermanentMod();
            //либо увеличиваем физ дамаг в способности
            //  player.defaultDamage.Damage.PhysicalDamage += _increaseDamage;

            var damageComponent = Player.damage;

            damageComponent.MainInfluence.IncreaseDamage(DamageType.Physical, Settings.Value);

        }
    }
}