using System.Collections.Generic;
using System.Linq;
using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.LevelBuff {
    public class ColdAdditionalDamagePower : AdditionalDamagePower<ColdAdditionalDamageSkillSettings>, IInfluenceApplier
    {
        void IInfluenceApplier.Apply(UnitsEntity attacker, UnitsEntity target)
        {
            if(target.activeUnitBuff.Any(o=> o is FreezeUnitBuf)) return;
            
            target.ApplyBuf(new FreezeUnitBuf(Settings.DecelerationPercent));
        }

        public override void DoLevelPowerActivate()
        {
            base.DoLevelPowerActivate();
            this.Player.AddMoreDamage(this);
        }

        public override void DoLevelPowerDeActivate()
        {
            base.DoLevelPowerDeActivate();
            var list = this.Player.otherDamage.Collection;
            list.Remove(this);
            this.Player.ReplaceOtherDamage(list); // обновили сущность 
        }

        public ColdAdditionalDamagePower(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext, IUnitDamageApplierFactory factory) : base(provider, unitsContext, factory) { }
    }
}