using Core.Data.Provider;
using GameKit;
using RoyalAxe.CharacterStat;
using UnityEngine;

namespace RoyalAxe.Configs
{
    //Cюда добавлять всякие найстройки стата (видимость для игрока, префаб иконки, какой нибудь обработчик чего либо.)
    [AllowMultiItems]
    public class CharacterStatTypeParameters : ScriptableObject, IDataObject
    {
        [field: SerializeField] public CharacterStatType Stat { get; private set; }
        public string UniqueID => name;
        [SerializeField] public CharacterStatValue DefaultValue = CharacterStatValue.DefaultMax100Current0Min0;

        // public int UnitsComponentsLookupIndex => GetIndex(Stat);

        /*public static int GetIndex(CharacterStatType stat)
        {
            switch (stat)
            {
                case CharacterStatType.Health:
                    return UnitsComponentsLookup.Health;
                case CharacterStatType.Armor:
                    return UnitsComponentsLookup.Armor;
                case CharacterStatType.Damage:
                    return UnitsComponentsLookup.Damage;
                case CharacterStatType.AttackSpeed:
                    return UnitsComponentsLookup.AttackSpeed;
                case CharacterStatType.StockHeals:
                    return UnitsComponentsLookup.StockHeals;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stat), stat, null);
            }
        }*/
    }
}