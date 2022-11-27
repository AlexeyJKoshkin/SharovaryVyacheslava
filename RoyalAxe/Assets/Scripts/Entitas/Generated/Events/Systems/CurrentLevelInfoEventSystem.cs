//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class CurrentLevelInfoEventSystem : Entitas.ReactiveSystem<CoreGamePlayEntity> {

    readonly System.Collections.Generic.List<ICurrentLevelInfoListener> _listenerBuffer;

    public CurrentLevelInfoEventSystem(Contexts contexts) : base(contexts.coreGamePlay) {
        _listenerBuffer = new System.Collections.Generic.List<ICurrentLevelInfoListener>();
    }

    protected override Entitas.ICollector<CoreGamePlayEntity> GetTrigger(Entitas.IContext<CoreGamePlayEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(CoreGamePlayMatcher.CurrentLevelInfo)
        );
    }

    protected override bool Filter(CoreGamePlayEntity entity) {
        return entity.hasCurrentLevelInfo && entity.hasCurrentLevelInfoListener;
    }

    protected override void Execute(System.Collections.Generic.List<CoreGamePlayEntity> entities) {
        foreach (var e in entities) {
            var component = e.currentLevelInfo;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.currentLevelInfoListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnCurrentLevelInfo(e, component.Level);
            }
        }
    }
}