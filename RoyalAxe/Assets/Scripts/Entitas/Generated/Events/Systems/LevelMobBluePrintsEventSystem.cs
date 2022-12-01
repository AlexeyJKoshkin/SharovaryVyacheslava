//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class LevelMobBluePrintsEventSystem : Entitas.ReactiveSystem<CoreGamePlayEntity> {

    readonly System.Collections.Generic.List<ILevelMobBluePrintsListener> _listenerBuffer;

    public LevelMobBluePrintsEventSystem(Contexts contexts) : base(contexts.coreGamePlay) {
        _listenerBuffer = new System.Collections.Generic.List<ILevelMobBluePrintsListener>();
    }

    protected override Entitas.ICollector<CoreGamePlayEntity> GetTrigger(Entitas.IContext<CoreGamePlayEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(CoreGamePlayMatcher.LevelMobBluePrints)
        );
    }

    protected override bool Filter(CoreGamePlayEntity entity) {
        return entity.hasLevelMobBluePrints && entity.hasLevelMobBluePrintsListener;
    }

    protected override void Execute(System.Collections.Generic.List<CoreGamePlayEntity> entities) {
        foreach (var e in entities) {
            var component = e.levelMobBluePrints;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.levelMobBluePrintsListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnLevelMobBluePrints(e, component.Collection);
            }
        }
    }
}
