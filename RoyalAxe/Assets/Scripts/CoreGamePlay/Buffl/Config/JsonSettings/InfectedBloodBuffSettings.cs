using System;
using Core.Parser;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class InfectedBloodBuffSettings: BaseLevelBuffSettings
    {
        private SkillConfigDef.Damage Damage;
        [ColumnName("Number_infections")]
        public int NumberInfections;


        public InfectedBloodBuffSettings() : base(LevelBuffType.InfectedBlood) { }
    }
}