using System;

namespace RoyalAxe.CharacterStat
{
    [Serializable]
    public class WeaponsSkillConfigDef : SkillConfigDef
    {
        public WeaponsSkillConfigDef() { }

        public WeaponsSkillConfigDef(string id, int lvl) : base(id, lvl) { }
    }
}