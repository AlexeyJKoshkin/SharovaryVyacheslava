using Entitas;
using Entitas.CodeGeneration.Attributes;
using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.GameEntitas.Timer
{
    public delegate void TickTimerCallback(IRATimer entity);

    public interface ITimerInfo
    {
        bool IsDone { get; }
        float Left { get; }
        float Percent { get; }
        float Counter { get; }
        float DestinationTime { get; }
    }

    [GameRootLoop, Event(EventTarget.Self)]
    public class TimerComponent : IComponent, ITimerInfo
    {
        public bool IsDone => Counter >= DestinationTime;
        public float Left => DestinationTime - Counter;
        public float Percent => Counter / DestinationTime;
        public float Counter { get; set; }
        public float DestinationTime { get; set; }

        public override string ToString()
        {
            return $"[{Counter}/{DestinationTime}]  {Percent} % Left {Left}";
        }
    }

    [GameRootLoop, Event(EventTarget.Self)]
    public class DoneTimerComponent : IComponent { }

    [GameRootLoop]
    public class RepeatComponent : IComponent { }

    public class ActiveTimerComponent : IComponent { }

    [GameRootLoop]
    public class PauseComponent : IComponent { }
}