using System.Collections.Generic;
using Core.UserProfile;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using GameKit;
using RoyalAxe.CoreLevel;
using RoyalAxe.LevelBuff;

namespace RoyalAxe.GameEntitas 
{
    [CoreGamePlay]
    public class CurrentLevelInfoComponent : IComponent
    {
        public LastLevel Level;
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
