using System.Collections.Generic;
using System.Linq;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.GameEntitas 
{
    public interface IUnitMainItem :  IWeaponItem,IEquipItem
    {
        void IncreaseDamage(SkillConfigDef.Damage damage);
        //Усиливаем урон от урона на абсолютную величину
        void IncreaseDamage(DamageType type, float amount);
        
        void DecreaseDamage(SkillConfigDef.Damage damage);
        //Усиливаем урон от урона на абсолютную величину
        void DecreaseDamage(DamageType type, float amount);
    }

    public class MainWeaponItem : BaseUnitItemWeapon,IUnitMainItem
    {
        private readonly InfluenceApplierComposite _composite;
        private readonly IUnitDamageApplierFactory _unitDamageApplierFactory;
        
        public MainWeaponItem(InfluenceApplierComposite composite, IUnitDamageApplierFactory unitDamageApplierFactory)
        {
            _composite = composite;
            _unitDamageApplierFactory = unitDamageApplierFactory;
        }

        public void IncreaseDamage(DamageType type, float settingsValue)
        {
            _composite.IncreaseDamage(type, settingsValue);
        }

        public void IncreaseDamage(SkillConfigDef.Damage damage)
        {
            IncreaseSimpleDamage(damage, 1);
            
            if (damage.ElementalDamage > 0 && damage.DamageCooldown > 0) // есть елементальный урон размазанный по времени
            {
                var timeDamage = _unitDamageApplierFactory.CreatePeriodicDamage(damage);
                
                _composite.PeriodicDamage.Add(timeDamage);
            }
        }

        public void DecreaseDamage(SkillConfigDef.Damage damage)
        {
            IncreaseSimpleDamage(damage, -1);
            if (damage.ElementalDamage > 0 && damage.DamageCooldown > 0) // есть елементальный урон размазанный по времени
            {
                var applier = _composite.PeriodicDamage.FirstOrDefault(o => o.DamageData == damage);
                if (applier != null)
                    _composite.PeriodicDamage.Remove(applier);
            }
        }

        public void DecreaseDamage(DamageType type, float amount)
        {
            _composite.IncreaseDamage(type,-amount);
        }

        void IncreaseSimpleDamage(SkillConfigDef.Damage damage, int sign)
        {
            this.IncreaseDamage(DamageType.Physical, -damage.PhysicalDamage);
            if (damage.ElementalDamage > 0 && damage.DamageCooldown <= 0) // есть одномоментный магический урон
            {
                IncreaseDamage(damage.ElementalDamageType,damage.ElementalDamage*sign);
            }
        }

        #region IWeaponItem
        void IWeaponItem.AttackTarget(UnitsEntity target)
        {
            _composite.Apply(Owner, target);
        }
        
        float  IWeaponItem.GetSingleValue(DamageType type)
        {
            return _composite.GetSingleValue(type);
        }
        #endregion
     
    }
}