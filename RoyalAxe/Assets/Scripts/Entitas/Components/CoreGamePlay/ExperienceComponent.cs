using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RoyalAxe.GameEntitas
{
    [CoreGamePlay, Event(EventTarget.Self)]
    public class ExperienceComponent : IComponent
    {
        public int Value;
    }
    
    [CoreGamePlay, Event(EventTarget.Self)]
    public class GoldComponent : IComponent
    {
        public int Value;
    }
    
    [CoreGamePlay, Event(EventTarget.Self)]
    public class GemsComponent : IComponent
    {
        public int Value;
    }

   
}
