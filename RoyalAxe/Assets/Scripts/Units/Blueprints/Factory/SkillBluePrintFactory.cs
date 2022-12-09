using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel {
    public class SkillBluePrintFactory : ISkillBlueprintsFactory
    {
        public SkillBlueprint Create(WeaponsSkillConfigDef weaponData, int level, string id)
        {
            var weaponByLevel = weaponData.GetByLevel(level);
            return new SkillBlueprint(id, level)
            {
                DamageData = weaponByLevel.damage,
                RangeData  = weaponByLevel.rangeParams
            };
        }
    }
}