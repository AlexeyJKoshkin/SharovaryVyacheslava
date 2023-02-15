using UnityEngine;

namespace RoyalAxe.Configs
{
    //пока будет так. Пока не придумаем как перейти на EntitasComponent.XXXXXXCoponent
    public enum CharacterStatType
    {
        [UnitsComponentsEnum]
        Health = UnitsComponentsLookup.Health,
        AttackSpeed = UnitsComponentsLookup.AttackSpeed,
        MoveSpeed = UnitsComponentsLookup.MoveSpeed
    }

    public class UnitsComponentsEnumAttribute : PropertyAttribute
    {
        public string Components;
    }
}