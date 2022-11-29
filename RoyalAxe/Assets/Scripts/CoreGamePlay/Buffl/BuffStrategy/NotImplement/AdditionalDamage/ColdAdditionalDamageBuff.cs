using System.Linq;
using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelBuff {
    public class ColdAdditionalDamageBuff : AdditionalDamageBuff<ColdAdditionalDamageBuffSettings>, IInfluenceApplierComposite
    {
        public ColdAdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, IUnitDamageApplierFactory unitDamageApplierFactory, UnitsContext unitsContext) : base(provider, unitDamageApplierFactory, unitsContext) { }
        
        
        void IInfluenceApplier.Apply(UnitsEntity attacker, UnitsEntity target)
        {
            if(target.activeUnitBuff.Any(o=> o is FreezeUnitBuf)) return;
            
            target.ApplyBuf(new FreezeUnitBuf(Settings.DecelerationPercent));
        }

        void IInfluenceApplierComposite.IncreaseDamage(DamageType physical, float settingsValue)
        {
        }

        public override void DoBuffStrategyActivate()
        {
            base.DoBuffStrategyActivate(); // в базе добавили урон
            this.Player.damage.Add(this); // добавили самого себя, в качестве сущности, которая будет навешивать баф/заморозку
        }
    }
}