using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RoyalAxe.GameEntitas
{
    [CoreGamePlay, Event(EventTarget.Self)]
    public class EarnedExperienceComponent : IComponent
    {
        public int Value;
    }
    
    [CoreGamePlay, Event(EventTarget.Self)]
    public class EarnedGoldComponent : IComponent
    {
        public int Value;
    }
    
    [CoreGamePlay, Event(EventTarget.Self)]
    public class EarnedGemsComponent : IComponent
    {
        public int Value;
    }
}
