using Core.Data.Provider;
using RoyalAxe.Configs;
using UnityEngine;

namespace RoyalAxe.CharacterStat
{
    public abstract class SkillsSettings : ScriptableObject, IDataObject
    {
        public float Size = 10;
        public abstract string UniqueID { get; }

        public UnitConfigDef BosonConfig;
    }
}