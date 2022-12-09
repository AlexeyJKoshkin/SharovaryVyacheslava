using System.Collections.Generic;

namespace RoyalAxe.CoreLevel {
    public interface IMobBlueprintsSpawnStorage
    {
        void PrepareBlueprints(IEnumerable<MobAtLevelData> selectMany);
        IEnumerable<GenerateMobBlueprintCounter> CreateWavePack(List<MobAtLevelData> mobsData);
    }
}