using System.Collections.Generic;
using Core.UserProfile;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using GameKit;
using RoyalAxe.CoreLevel;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.GameEntitas 
{
    [CoreGamePlay,Event(EventTarget.Self)]
    public class CurrentLevelInfoComponent : IComponent
    {
        public LastLevel Level;
    }


    [CoreGamePlay]
    [Unique]
    public class LevelWaveComponent : IComponent
    {
    }

    [CoreGamePlay]
    public class LevelWaveQueueComponent : IComponent
    {
        public Queue<LevelSettingsData> Queue;
        public LevelSettingsData Current => Queue.Peek();
    }
    

    [CoreGamePlay,Event(EventTarget.Self)]
    public class LevelMobBluePrints : ListCollectionComponent<GenerateMobBlueprintCounter>
    {
        public MobBlueprint GenerateMobData()
        {
             var item = this.Collection.GetRandom(false);
                       item.TotalAmount--;
                       if (item.TotalAmount == 0) this.Remove(item);
                       return item.MobBlueprint;
        }
    }
    
    

    [CoreGamePlay,Event(EventTarget.Self)]
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
