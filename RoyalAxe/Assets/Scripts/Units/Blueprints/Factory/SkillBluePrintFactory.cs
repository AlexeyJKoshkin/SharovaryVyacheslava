using RoyalAxe.Units.Stats;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel {
    public class SkillBluePrintFactory : ISkillBlueprintsFactory
    {
        public SkillBlueprint Create(WeaponsSkillConfigDef weaponData, int level)
        {
            var weaponByLevel = weaponData.GetByLevel(level);
            return new SkillBlueprint(weaponData.UniqueID, level)
            {
                DamageData = weaponByLevel.damage,
                RangeData  = weaponByLevel.rangeParams
            };
        }
    }
}