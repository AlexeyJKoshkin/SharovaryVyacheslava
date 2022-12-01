using System.Collections.Generic;
using System.Linq;
using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelBuff {
    public class ColdAdditionalDamagePower : AdditionalDamagePower<ColdAdditionalDamageBuffSettings>, IInfluenceApplier
    {
        public ColdAdditionalDamagePower(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext) : base(provider, unitsContext) { }
        
        
        void IInfluenceApplier.Apply(UnitsEntity attacker, UnitsEntity target)
        {
            if(target.activeUnitBuff.Any(o=> o is FreezeUnitBuf)) return;
            
            target.ApplyBuf(new FreezeUnitBuf(Settings.DecelerationPercent));
        }

        public override void DoLevelPowerActivate()
        {
            base.DoLevelPowerActivate();
            AddOtherDamage(this);
        }

        public override void DoLevelPowerDeActivate()
        {
            base.DoLevelPowerDeActivate();
            var list = this.Player.otherDamage.Collection;
            list.Remove(this);
            this.Player.ReplaceOtherDamage(list); // обновили сущность 
        }
    }
}