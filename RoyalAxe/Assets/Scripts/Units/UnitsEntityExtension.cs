using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    public static class UnitsEntityExtension
    {
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

        public static void ApplyBuf(this UnitsEntity unit, IEntityBuff buff)
        {
            //нельзя повесить пустой баф или тот который уже весит
            if (unit == null || buff == null || unit.activeUnitBuff.Contains(buff))
            {
                return;
            }

            unit.ReplaceActiveUnitBuff(unit.activeUnitBuff.Add(buff));
            buff.ApplyTo(unit);
        }

        public static void RemoveBuf(this UnitsEntity unit, IEntityBuff buff)
        {
            if (unit == null || buff == null)
            {
                return;
            }

            unit.ReplaceActiveUnitBuff(unit.activeUnitBuff.Remove(buff));
            buff.RemoveFrom(unit);
        }

        public static void Equip(this UnitsEntity unit, SlotType mainWeapon, IEquipItem activeWeapon)
        {
            if (unit == null || activeWeapon == null)
            {
                return;
            }

            //по идее тут проверки, что в слоте ничего нету и мы можем туда что-то экипировать
            //далее передаем себя в вещь, чтобы она применила модификаторы и бафы на хозяина
            activeWeapon.ApplyTo(unit);
            if (mainWeapon == SlotType.MainWeapon) // как-то определяем что это оружие, которым можно пользоваться
            {
                //  ActiveWeapon.Add(activeWeapon); // тоже самое что и в бафах
            }
        }

        public static void Equip(this UnitsEntity unit, IWeaponItem activeWeapon)
        {
            Equip(unit, activeWeapon.AvailableSlot, activeWeapon);
            // ActiveWeapon.Add(activeWeapon); // тоже самое что и в бафах
        }
    }
}