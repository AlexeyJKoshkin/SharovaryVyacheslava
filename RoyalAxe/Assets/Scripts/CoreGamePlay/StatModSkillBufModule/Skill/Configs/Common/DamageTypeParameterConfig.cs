using System;
using Core.Data.Provider;
using GameKit;
using UnityEngine;

namespace RoyalAxe.CharacterStat
{
    [AllowMultiItems]
    public class DamageTypeParameterConfig : ScriptableObject, IDataObject
    {
        public string UniqueID { get; }
        [field: SerializeField] public DamageType Type { get; protected set; }
    }

    [Serializable]
    public enum DamageType
    {
        Physical = 0,
        Fire = 1,
        Poison = 2,
        Cold = 3,
        Blood = 4
    }
}