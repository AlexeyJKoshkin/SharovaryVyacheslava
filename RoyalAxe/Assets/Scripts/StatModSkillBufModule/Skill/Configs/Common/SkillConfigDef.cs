using System;
using System.Collections.Generic;
using Core.Data.Provider;
using Core.Parser;
using Newtonsoft.Json;
using UnityEngine;

namespace RoyalAxe.CharacterStat
{
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
            [JsonProperty("pd"), ColumnName("Physic_damage")]
            public float PhysicalDamage;

            [JsonProperty("md"), ColumnName("Magic_damage")]
            public float ElementalDamage;

            //Длительность елементального воздействия
            [JsonProperty("mc"), ColumnName("Cooldown_magic")]
            public float MagicDuration;

            /// <summary>
            ///     Кулдаун нанесения урона
            /// </summary>
            [JsonProperty("CRA"), ColumnName("Cooldown_range_attack")]
            public float DamageCooldown;

            [JsonProperty("tm"), ColumnName("Type_magic")]
            public DamageType ElementalDamageType;

        
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