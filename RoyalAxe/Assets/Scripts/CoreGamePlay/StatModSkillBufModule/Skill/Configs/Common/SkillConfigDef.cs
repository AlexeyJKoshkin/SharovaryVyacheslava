using System;
using System.Collections.Generic;
using Core.Data.Provider;
using Core.Parser;
using Newtonsoft.Json;
using UnityEngine;

namespace RoyalAxe.Units.Stats
{
    [Serializable]
    public struct SingleDamageInfo
    {
        [JsonProperty("md"), ColumnName("Magic_damage")]
        public float Value;
        [JsonProperty("tm"), ColumnName("Type_magic")]
        public DamageType DamageType;
    }
    
    [Serializable]
    public abstract class SkillConfigDef : IDataObject
    {
        public string UniqueID { get; set; }

        public SkillConfigDef() { }

        public SkillConfigDef(string id, int lvl)
        {
            UniqueID    = id;
            SkillDamage = new List<Damage>(lvl);
            RangeConfig = new List<RangeParams>(lvl);
        }

        [SerializeField] public List<Damage> SkillDamage = new List<Damage>();

        [SerializeField] public List<RangeParams> RangeConfig = new List<RangeParams>();

        public int TotalLevels => SkillDamage.Count;
        
        public (Damage damage, RangeParams rangeParams) GetByLevel(int lvl)
        {
            lvl--; // уровнь всегда на 1 больше чем индекс
            if (lvl < SkillDamage.Count)
            {
                return (SkillDamage[lvl], RangeConfig[lvl]);
            }

            return (new Damage(), new RangeParams());
        }

        
        [Serializable]
        public class Damage
        {
            [JsonProperty("md"), ColumnName("Magic_damage")]
            public float ElementalDamage;
            [JsonProperty("tm"), ColumnName("Type_magic")]
            public DamageType ElementalDamageType;

            public SingleDamageInfo SingleDamageInfo => new SingleDamageInfo() {DamageType = ElementalDamageType, Value = ElementalDamage};
            
            [JsonProperty("pd"), ColumnName("Physic_damage")]
            public float PhysicalDamage;
          
            //Длительность елементального воздействия
            [JsonProperty("mc"), ColumnName("Cooldown_magic")]
            public float MagicDuration;

            /// <summary>
            ///     Кулдаун нанесения урона
            /// </summary>
            [JsonProperty("CRA"), ColumnName("Cooldown_range_attack")]
            public float DamageCooldown;

            public float CriticalChance;
            public float CriticalDamage;

        }

        [Serializable]
        public class RangeParams
        {
            //Кулдаун стрельбы
            [JsonProperty("C"), ColumnName("Cooldown_attack")]
            public float RangeCooldownAttack;

            /// <summary>
            ///     Скорость снаряда
            /// </summary>
            [JsonProperty("sf"), ColumnName("Speed_fire")]
            public float MissileSpeed;
            
            [ ColumnName("Start_use_count")]
            [JsonProperty("su")]
            public int StartUsage = 1;
            
            [JsonProperty("pu")]
            public int PriceUsage = 1;
        }
    }
}