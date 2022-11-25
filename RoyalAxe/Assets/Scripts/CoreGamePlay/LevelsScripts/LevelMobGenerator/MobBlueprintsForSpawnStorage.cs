using System.Collections.Generic;
using System.Linq;
using Core.Data.Provider;
using GameKit;
using RoyalAxe.CharacterStat;
using RoyalAxe.Configs;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel
{
    public interface IMobBlueprintsSpawnStorage
    {
        void PrepareBlueprints(IEnumerable<MobAtLevelData> selectMany);
        IEnumerable<GenerateMobBlueprintCounter> CreateWavePack(List<MobAtLevelData> mobsData);
    }

    public class MobBlueprintsForSpawnStorage : IMobBlueprintsSpawnStorage
    {
        private readonly IDataStorage _dataStorage;


        private readonly Dictionary<string, Dictionary<int, MobBlueprint>> _cashedMobPrints = new Dictionary<string, Dictionary<int, MobBlueprint>>();

        public MobBlueprintsForSpawnStorage(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public void PrepareBlueprints(IEnumerable<MobAtLevelData> allMobData)
        {
            _cashedMobPrints.Clear();

            foreach (var e in allMobData.GroupBy(o => o.MobId))
            {
                AddToCache(e);
            }
        }

        public IEnumerable<GenerateMobBlueprintCounter> CreateWavePack(List<MobAtLevelData> mobsData)
        {
            foreach (var md in mobsData)
            {
                var mobBluePrint = _cashedMobPrints[md.MobId][md.Level];

                yield return new GenerateMobBlueprintCounter()
                {
                    MobBlueprint = mobBluePrint,
                    TotalAmount  = md.TotalAmount
                };
            }
        }


        private void AddToCache(IGrouping<string, MobAtLevelData> mobs)
        {
            var mobStatCollection = _dataStorage.ById<StatCollection>(mobs.Key);
            var weaponData        = _dataStorage.ById<WeaponsSkillConfigDef>(mobs.Key);
            var dic               = Create(mobs);
            _cashedMobPrints.Add(mobs.Key, dic);

            Dictionary<int, MobBlueprint> Create(IEnumerable<MobAtLevelData> mobAtLevelData)
            {
                var dic = new Dictionary<int, MobBlueprint>();
                foreach (var levelGroup in mobAtLevelData.GroupBy(o=> o.Level))
                {
                    var level = levelGroup.Key;
                    var weaponByLevel = weaponData.GetByLevel(level);
                    var mobBluePrint = new MobBlueprint(mobs.Key, level)
                    {
                        Stats = mobStatCollection.GetByLevel(level),
                        ActiveSkill = new SkillBlueprint(mobs.Key, level)
                        {
                            DamageData = weaponByLevel.damage,
                            RangeData  = weaponByLevel.rangeParams
                        }
                    };
                    dic.Add(level, mobBluePrint);
                }

                return dic;
            }
        }
    }
}