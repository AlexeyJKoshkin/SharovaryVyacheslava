using System.Collections.Generic;
using System.Linq;
using GameKit;
using RoyalAxe.CharacterStat;
using RoyalAxe.Configs;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel
{
    public class MobBlueprintsForSpawnStorage : IMobBlueprintsSpawnStorage
    {
        
        private readonly IUnitsBlueprintsFactory _blueprintsFactory;

        private readonly Dictionary<string, IDictionary<int, MobBlueprint>> _cashedMobPrints = new Dictionary<string, IDictionary<int, MobBlueprint>>();

        public MobBlueprintsForSpawnStorage(IUnitsBlueprintsFactory blueprintsFactory)
        {
            _blueprintsFactory = blueprintsFactory;
        }

        // подготовить блупринты мобов для уровня
        public void PrepareBlueprints(IEnumerable<MobAtLevelData> allMobData)
        {
            _cashedMobPrints.Clear();

            foreach (var e in allMobData.GroupBy(o => o.MobId))
            {
                var dic = _blueprintsFactory.CreateMobBluePrints(e.Key, e);
                _cashedMobPrints.Add(e.Key,dic);
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
        
    }
}