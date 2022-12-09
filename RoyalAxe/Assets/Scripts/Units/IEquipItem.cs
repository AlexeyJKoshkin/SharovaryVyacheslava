using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    public interface IEquipItem : IUnitApplierItem
    {
        SlotType AvailableSlot { get; }
    }

    public interface IWeaponItem
    {
        void AttackTarget(UnitsEntity target);
        
       
        float GetSingleValue(DamageType type);
    }
    
    


    public enum SlotType
    {
        Helm,
        MainWeapon,
        Chest
    }
}