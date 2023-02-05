using RoyalAxe.Units.Stats;

namespace RoyalAxe.GameEntitas {
    public class SkillBlueprint : BaseBlueprint
    {
        public SkillConfigDef.RangeParams RangeData;
        public SkillConfigDef.Damage DamageData;


        public SkillBlueprint(string id, int level):base(id, level)
        {
        }
    }

    public class WeaponBluePrint : BaseBlueprint
    {
        public SkillBlueprint SkillBlueprint;
        public float CriticalChance;
        public float CriticalDamage;
    }
}