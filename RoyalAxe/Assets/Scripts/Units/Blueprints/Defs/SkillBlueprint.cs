using Core.UserProfile;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas {
    public class SkillBlueprint : BaseBlueprint
    {
        public SkillConfigDef.RangeParams RangeData;
        public SkillConfigDef.Damage DamageData;

        public SkillBlueprint(SaveEntityRecord weaponRecord):this(weaponRecord.Id, weaponRecord.Level)
        {
        }

        public SkillBlueprint(string id, int level):base(id, level)
        {
        }
    }
}