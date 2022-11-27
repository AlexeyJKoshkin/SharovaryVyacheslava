//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreGamePlayEntity {

    public RoyalAxe.GameEntitas.LevelWaveQueueComponent levelWaveQueue { get { return (RoyalAxe.GameEntitas.LevelWaveQueueComponent)GetComponent(CoreGamePlayComponentsLookup.LevelWaveQueue); } }
    public bool hasLevelWaveQueue { get { return HasComponent(CoreGamePlayComponentsLookup.LevelWaveQueue); } }

    public void AddLevelWaveQueue(System.Collections.Generic.Queue<RoyalAxe.CoreLevel.LevelSettingsData> newQueue) {
        var index = CoreGamePlayComponentsLookup.LevelWaveQueue;
        var component = (RoyalAxe.GameEntitas.LevelWaveQueueComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.LevelWaveQueueComponent));
        component.Queue = newQueue;
        AddComponent(index, component);
    }

    public void ReplaceLevelWaveQueue(System.Collections.Generic.Queue<RoyalAxe.CoreLevel.LevelSettingsData> newQueue) {
        var index = CoreGamePlayComponentsLookup.LevelWaveQueue;
        var component = (RoyalAxe.GameEntitas.LevelWaveQueueComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.LevelWaveQueueComponent));
        component.Queue = newQueue;
        ReplaceComponent(index, component);
    }

    public void RemoveLevelWaveQueue() {
        RemoveComponent(CoreGamePlayComponentsLookup.LevelWaveQueue);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class CoreGamePlayMatcher {

    static Entitas.IMatcher<CoreGamePlayEntity> _matcherLevelWaveQueue;

    public static Entitas.IMatcher<CoreGamePlayEntity> LevelWaveQueue {
        get {
            if (_matcherLevelWaveQueue == null) {
                var matcher = (Entitas.Matcher<CoreGamePlayEntity>)Entitas.Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayComponentsLookup.LevelWaveQueue);
                matcher.componentNames = CoreGamePlayComponentsLookup.componentNames;
                _matcherLevelWaveQueue = matcher;
            }

            return _matcherLevelWaveQueue;
        }
    }
}
