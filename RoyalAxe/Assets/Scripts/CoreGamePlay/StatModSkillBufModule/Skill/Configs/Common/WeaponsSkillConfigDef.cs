using System;

namespace RoyalAxe.Units.Stats
{
    [Serializable]
    public class WeaponsSkillConfigDef : SkillConfigDef
    {
        public WeaponsSkillConfigDef() { }

        public WeaponsSkillConfigDef(string id, int lvl) : base(id, lvl) { }
    }
}