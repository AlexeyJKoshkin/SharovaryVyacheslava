using Core.UserProfile;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RoyalAxe.GameEntitas {
    [CoreGamePlay, Event(EventTarget.Self)]
    public class CurrentLevelInfoComponent : IComponent
    {
        public LastLevel Level;
    }
}