using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    public interface IEquipItem : IEntityBuff
    {
        SlotType AvailableSlot { get; }
    }

    public interface IWeaponItem : IEquipItem { }


    public enum SlotType
    {
        Helm,
        MainWeapon,
        Chest
    }
}