using System.Collections.Generic;
using GameKit;
using RoyalAxe.CoreLevel;

namespace RoyalAxe.GameEntitas {
    [CoreGamePlay]
    public class MobWaveCollectionComponent : AbstractCollectionComponent<List<MobAtLevelData>, MobAtLevelData>
    {
        public bool HasMobs => Count > 0;

        /*public void Load(MobAtLevelData[] mobData)
        {
            _list.Clear();
            _list.AddRange(mobData);
        }*/

        public MobAtLevelData Generate()
        {
            var item = this.Collection.GetRandom(false);
            item.TotalAmount--;
            if (item.TotalAmount == 0) this.Remove(item);
            return item;
        }
    }
}