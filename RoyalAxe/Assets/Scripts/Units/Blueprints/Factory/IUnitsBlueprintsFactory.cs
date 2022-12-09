using System.Collections.Generic;
using Core.UserProfile;

using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel {
    public interface IUnitsBlueprintsFactory
    {
       // MobBlueprint CreateMobBluePrint(WeaponsSkillConfigDef weaponData, StatCollection mobStatCollection, int level, string id);
        IDictionary<int, MobBlueprint> CreateMobBluePrints(string eKey, IEnumerable<MobAtLevelData> mobs);
        UnitBlueprint CreatePlayerBluePrint(HeroProgressData heroRecord, SaveEntityRecord weaponRecord);
    }
}