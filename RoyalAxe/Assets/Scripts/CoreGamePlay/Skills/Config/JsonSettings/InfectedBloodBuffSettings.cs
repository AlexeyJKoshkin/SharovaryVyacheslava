using System;
using Core.Parser;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.LevelBuff
{
    [Serializable]
    public class InfectedBloodSkillSettings: BaseLevelSkillSettings
    {
        private SkillConfigDef.Damage Damage;
        [ColumnName("Number_infections")]
        public int NumberInfections;


        public InfectedBloodSkillSettings() : base(LevelSkillType.InfectedBlood) { }
    }
}