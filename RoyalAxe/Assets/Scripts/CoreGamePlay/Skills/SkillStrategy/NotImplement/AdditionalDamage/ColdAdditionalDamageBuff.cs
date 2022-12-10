using System.Linq;
using Core;
using RoyalAxe.GameEntitas;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.LevelSkill {
    public class ColdAdditionalDamagePlayerSkill : AdditionalDamagePlayerSkill<ColdAdditionalDamageSkillSettings>,IWeaponItem
    {
        private IBuffFactory _unitsBuffBuilder;
        
        public ColdAdditionalDamagePlayerSkill(ILevelBuffSettingCompositeProvider provider, UnitsContext unitsContext, IUnitDamageApplierFactory factory, IBuffFactory unitsBuffBuilder) : base(provider, unitsContext, factory)
        {
            _unitsBuffBuilder = unitsBuffBuilder;
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
           // asdasd
            if(target.activeUnitBuff.Any(o=> o.isFreezeBuff)) return;

            var freezebuf = _unitsBuffBuilder.BuildFreezeUnitBuf(this.Player,Settings.DecelerationPercent);
            target.ApplyBuf(freezebuf); // надо подумать, о том, что нужен будет билдер баффов*/
        }

        float IWeaponItem.GetSingleValue(DamageType type)
        {
            return 0; // Скилл не атакует. только навешивает баф.
        }
        #endregion
    }
    
    
}