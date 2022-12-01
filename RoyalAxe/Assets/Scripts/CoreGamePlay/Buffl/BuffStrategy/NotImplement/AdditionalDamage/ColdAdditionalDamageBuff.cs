using System.Collections.Generic;
using System.Linq;
using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelBuff {
    public class ColdAdditionalDamageBuff : AdditionalDamageBuff<ColdAdditionalDamageBuffSettings>, IInfluenceApplierComposite
    {
        public ColdAdditionalDamageBuff(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext) : base(provider, unitsContext) { }
        
        
        void IInfluenceApplier.Apply(UnitsEntity attacker, UnitsEntity target)
        {
            if(target.activeUnitBuff.Any(o=> o is FreezeUnitBuf)) return;
            
            target.ApplyBuf(new FreezeUnitBuf(Settings.DecelerationPercent));
        }

        // баф невозможно улучшить.
        void IInfluenceApplierComposite.IncreaseDamage(DamageType physical, float settingsValue)
        {
        }

        public override void DoBuffStrategyActivate()
        {
            base.DoBuffStrategyActivate(); // в базе добавили урон
            var list = this.Player.hasOtherDamage ? this.Player.otherDamage.Collection : new List<IInfluenceApplier>();
            list.Add(this); // добавили самого себя, в качестве сущности, которая будет навешивать баф/заморозку
            
            this.Player.ReplaceOtherDamage(list); // обновили сущность 
        }
    }
}