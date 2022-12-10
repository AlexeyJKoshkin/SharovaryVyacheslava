using Entitas;
using Entitas.CodeGeneration.Attributes;
using RoyalAxe.Units.Stats;
using RoyalAxe.EntitasSystems.TimerUtility;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    [DontGenerate]
    public abstract class BaseTimerCounterComponent : IComponent
    {
        public IRATimer Timer;
        public bool IsPause => Timer.IsPause;
        public bool IsRunning => Timer.IsRunning;
        public bool IsRepeat => Timer.IsRepeat;

        public void Run()
        {
            Timer.IsRunning = true;
        }
    }

    [Skill]
    public class RestoreAttemptsTimerComponent : BaseTimerCounterComponent
    {
        public int RestoreAmount = 1; // под вопросом. возможно стоит перенести
    }

    [Skill]
    public class SkillReadyComponent : IComponent { }

    [Skill]
    public class SkillUseComponent : IComponent { }

    [Skill]
    public class GunnerMobSkillComponent : IComponent
    {
        public UnitsEntity Owner;
    }

    [Skill]
    public class DefaultPlayerSkillComponent : IComponent
    {
       
    }

    [Skill]
    public class DoubleAxeComponent : IComponent
    {
    }

    [Skill]
    public class TripleAxeComponent : IComponent
    {
    }

    [Skill]
    public class PriceUseSkillComponent : IComponent
    {
        public int Price;
    }

    [Skill, Event(EventTarget.Self)]
    public class UseCounterSkillComponent : IComponent
    {
        public int CurrentValue;
        public int MaxValue;
        public bool NeedRestore => CurrentValue < MaxValue;
        public bool CanUse => CurrentValue > 0;

        public override string ToString()
        {
            return $"{CurrentValue} / {MaxValue}";
        }
    }

  
}