//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class GamePauseEventSystem : Entitas.ReactiveSystem<GameRootLoopEntity> {

    readonly System.Collections.Generic.List<IGamePauseListener> _listenerBuffer;

    public GamePauseEventSystem(Contexts contexts) : base(contexts.gameRootLoop) {
        _listenerBuffer = new System.Collections.Generic.List<IGamePauseListener>();
    }

    protected override Entitas.ICollector<GameRootLoopEntity> GetTrigger(Entitas.IContext<GameRootLoopEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameRootLoopMatcher.GamePause)
        );
    }

    protected override bool Filter(GameRootLoopEntity entity) {
        return entity.hasGamePause && entity.hasGamePauseListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameRootLoopEntity> entities) {
        foreach (var e in entities) {
            var component = e.gamePause;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.gamePauseListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnGamePause(e, component.IsPause);
            }
        }
    }
}