using RoyalAxe.CharacterStat;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel {
    public interface ISkillBlueprintsFactory
    {
        SkillBlueprint Create(WeaponsSkillConfigDef weaponData, int level, string id);
    }
}