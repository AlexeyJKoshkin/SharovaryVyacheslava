using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using GameKit;
using RoyalAxe.CoreLevel;
using RoyalAxe.LevelBuff;

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
    [CoreGamePlay]
    [Unique]
    public class LevelWaveComponent : IComponent
    {
        
    }

    [CoreGamePlay, Event(EventTarget.Self)]
    public class LevelNumberComponent : IComponent
    {
        public int Number;
    }

    /// <summary>
    /// Указатель, что волна закончилась
    /// </summary>
    [CoreGamePlay]
    public class WaveFinishedComponent : IComponent
    {
    }


    
    [CoreGamePlay]
    public class WizardShopReadyComponent : IComponent
    {
        public LevelBuffType[] LevelBuffTypes;
    }
}
