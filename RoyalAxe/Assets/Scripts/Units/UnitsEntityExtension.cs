using System.Collections.Generic;
using GameKit;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.GameEntitas
{
    public static class UnitsEntityExtension
    {
        public static string HERO_POSTFIX = "_Hero";
        
        public static void AddMoreDamage(this UnitsEntity unit, IWeaponItem applier)
        {
            if(unit == null || applier == null) return;
            var list = unit.hasOtherDamage ? unit.otherDamage.Collection : new List<IWeaponItem>();
            list.Add(applier); // добавили эплайер себя, в качестве сущности, которая будет навешивать баф/заморозку

            unit.ReplaceOtherDamage(list); // обновили сущность 
        }

        public static void ApplyStatConstDamage(this UnitsEntity victim, int component, float damage)
        {
            if (!victim.HasComponent(component))
            {
                return;
            }

            var stat = victim.GetComponent(component) as ModifiableStat;
            stat.ChangeValue().ByConstValue(damage).ApplyPermanentMod();
            victim.ReplaceComponent(component, stat);
        }

        public static void ApplyBuf(this UnitsEntity unit, SkillEntity buff)
        {
            //нельзя повесить пустой баф или тот который уже весит
            if (unit == null || buff == null || unit.activeUnitBuff.Contains(buff))
            {
                return;
            }

            unit.ReplaceActiveUnitBuff(unit.activeUnitBuff.Add(buff));
            buff.ReplaceBuffTarget(unit);
            if(buff.hasBuffApplier)
                buff.buffApplier.ForEach(e=> e.ApplyTo(unit));
        }

        public static void RemoveBuf(this UnitsEntity unit, SkillEntity buff)
        {
            if (unit == null || buff == null)
            {
                return;
            }

            unit.ReplaceActiveUnitBuff(unit.activeUnitBuff.Remove(buff));
            if(buff.hasBuffApplier)
                buff.buffApplier.ForEach(e=> e.RemoveFrom(unit));
            buff.RemoveBuffTarget();
            buff.Destroy();
        }

      

        public static void EquipMainItem(this UnitsEntity unit, IUnitMainItem unitMainItem)
        {
            if(unit == null) return;
            if (unit.hasMainDamage) // уже экирирован
            {
                unit.mainDamage.Influence.RemoveFrom(unit); // тут снимуться все бафы
            }
            if(unitMainItem == null)
                unit.RemoveMainDamage();
            else
            {
                unitMainItem.ApplyTo(unit);
                unit.ReplaceMainDamage(unitMainItem);
            }
        }
  
    }
    
}