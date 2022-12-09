using System.Collections.Generic;
using System.Linq;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas 
{
    public interface IUnitMainItem :  IWeaponItem,IEquipItem
    {
        void IncreaseDamage(SkillConfigDef.Damage damage);
        //Усиливаем урон от урона на абсолютную величину
        void IncreaseDamage(DamageType physical, float settingsValue);
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
        
        public void AttackTarget(UnitsEntity target)
        {
            _composite.Apply(Owner, target);
        }

        public void IncreaseDamage(DamageType type, float settingsValue)
        {
            _composite.IncreaseDamage(type, settingsValue);
        }
        


        public void IncreaseDamage(SkillConfigDef.Damage damage)
        {
            this.IncreaseDamage(DamageType.Physical, damage.PhysicalDamage);
                        
            
            if (damage.ElementalDamage > 0 && damage.DamageCooldown <= 0) // есть одномоментный магический урон
            {
                IncreaseDamage(damage.ElementalDamageType,damage.ElementalDamage);
            }
            
            if (damage.ElementalDamage > 0 && damage.DamageCooldown > 0) // есть елементальный урон размазанный по времени
            {
                var timeDamage = _unitDamageApplierFactory.CreatePeriodicDamage(damage);
                
                _composite.PeriodicDamage.Add(timeDamage);
            }
        }

        public void DecreaseDamage(SkillConfigDef.Damage damage)
        {
            this.IncreaseDamage(DamageType.Physical, -damage.PhysicalDamage);
            if (damage.ElementalDamage > 0 && damage.DamageCooldown <= 0) // есть одномоментный магический урон
            {
                IncreaseDamage(damage.ElementalDamageType,-damage.ElementalDamage);
            }
            if (damage.ElementalDamage > 0 && damage.DamageCooldown > 0) // есть елементальный урон размазанный по времени
            {
                var applier = _composite.PeriodicDamage.FirstOrDefault(o => o.DamageData == damage);
                if (applier != null)
                    _composite.PeriodicDamage.Remove(applier);
            }
        }

        public float GetSingleValue(DamageType physical)
        {
            return _composite.GetSingleValue(physical);
        }

        
        public override void ApplyPermanentStatMods()
        {
            
        }

        protected override IEnumerable<ICharacterStatModificator> GetTemStats()
        {
            yield break;
        }

       
    }
}