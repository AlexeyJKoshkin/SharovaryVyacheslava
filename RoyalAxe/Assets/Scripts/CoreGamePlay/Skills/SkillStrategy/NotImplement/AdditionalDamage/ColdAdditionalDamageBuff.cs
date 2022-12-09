using System.Linq;
using Core;
using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;
using UnityEngine;

namespace RoyalAxe.LevelSkill {
    public class ColdAdditionalDamagePlayerSkill : AdditionalDamagePlayerSkill<ColdAdditionalDamageSkillSettings>,IWeaponItem
    {
        public ColdAdditionalDamagePlayerSkill(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext, IUnitDamageApplierFactory factory) : base(provider, unitsContext, factory)
        {
            HLogger.TempLog("Cold Skill Create");
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

        #region IWeaponItem
      
        void IWeaponItem.AttackTarget(UnitsEntity target)
        {
            if(target.activeUnitBuff.Any(o=> o is FreezeUnitBuf)) return;
            
            target.ApplyBuf(new FreezeUnitBuf(Settings.DecelerationPercent));
        }

        float IWeaponItem.GetSingleValue(DamageType type)
        {
            return 0; // Скилл не атакует. только навешивает баф.
        }
        #endregion
        
    }
}