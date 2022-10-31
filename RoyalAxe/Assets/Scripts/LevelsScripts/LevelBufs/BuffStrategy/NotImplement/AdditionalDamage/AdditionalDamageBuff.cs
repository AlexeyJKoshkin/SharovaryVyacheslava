using System.Linq;
using System.Runtime.InteropServices;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    public abstract class AdditionalDamageBuff<T> : AbstractBuffStrategy<T> where T : AdditionalDamageBuffSettings
    {
        private UnitsEntity Player => _unitsContext.playerEntity;

        private readonly UnitsContext _unitsContext;
        private readonly IUnitDamageApplierFactory _unitDamageApplierFactory;


        

        public override void DoBuffStrategyActivate()
        {
            var damageComponent = Player.damage;

            var maxPhysDamage = damageComponent.SingleDamage.Where(o => o.Type == DamageType.Physical).Max(o => o.Value);

            var damage = _unitDamageApplierFactory.CreateOneMomentDamage(Settings.DamageTypeType, maxPhysDamage * Settings.PercentActiveDamage * .01f);
            Player.damage.SingleDamage.Add(damage); 
            
        }

        protected AdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, IUnitDamageApplierFactory unitDamageApplierFactory, UnitsContext unitsContext) : base(provider)
        {
            _unitDamageApplierFactory = unitDamageApplierFactory;
            _unitsContext = unitsContext;
        }
    }
}