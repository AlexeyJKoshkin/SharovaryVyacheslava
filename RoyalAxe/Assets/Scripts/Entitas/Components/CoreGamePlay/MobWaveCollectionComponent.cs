using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
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
    [CoreGamePlay]
    [Unique]
    public class LevelWaveComponent : IComponent
    {
        
    }

    [CoreGamePlay, Event(EventTarget.Self)]
    public class WaveNumberComponent : IComponent
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

    //указательно, что можно начинать спавнить мобов
    [CoreGamePlay]
    public class WaveMobReadyComponent : IComponent
    {
    }
    
    [CoreGamePlay]
    public class WizardShopReadyComponent : IComponent
    {
    }
}