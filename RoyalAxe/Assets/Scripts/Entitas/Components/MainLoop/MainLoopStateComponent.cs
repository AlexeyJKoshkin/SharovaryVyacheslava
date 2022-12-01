using Core.Data.Provider;
using Core.Launcher;
using Core.UserProfile;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RoyalAxe.GameEntitas
{
    [GameRootLoop, Unique]
    public class MainLoopStateComponent : IComponent
    {
        public IProjectSceneState State;
    }

    [GameRootLoop, Unique, Event(EventTarget.Self)]
    public class GamePauseComponent : IComponent
    {
        public bool IsPause;
    }

    [GameRootLoop]
    public class AdditionalDataBoxComponent : AbstractCollectionComponent<DataBox[], DataBox> { }

    [GameRootLoop]
    public class UpdateSystemsComponent : AbstractCollectionComponent<Feature[], Feature> { }

    [GameRootLoop]
    public class PauseableUpdateSystemsComponent : UpdateSystemsComponent { }
}