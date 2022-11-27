//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreGamePlayEntity {

    public RoyalAxe.GameEntitas.LevelMobBluePrints levelMobBluePrints { get { return (RoyalAxe.GameEntitas.LevelMobBluePrints)GetComponent(CoreGamePlayComponentsLookup.LevelMobBluePrints); } }
    public bool hasLevelMobBluePrints { get { return HasComponent(CoreGamePlayComponentsLookup.LevelMobBluePrints); } }

    public void AddLevelMobBluePrints(System.Collections.Generic.List<RoyalAxe.CoreLevel.GenerateMobBlueprintCounter> newCollection) {
        var index = CoreGamePlayComponentsLookup.LevelMobBluePrints;
        var component = (RoyalAxe.GameEntitas.LevelMobBluePrints)CreateComponent(index, typeof(RoyalAxe.GameEntitas.LevelMobBluePrints));
        component.Collection = newCollection;
        AddComponent(index, component);
    }

    public void ReplaceLevelMobBluePrints(System.Collections.Generic.List<RoyalAxe.CoreLevel.GenerateMobBlueprintCounter> newCollection) {
        var index = CoreGamePlayComponentsLookup.LevelMobBluePrints;
        var component = (RoyalAxe.GameEntitas.LevelMobBluePrints)CreateComponent(index, typeof(RoyalAxe.GameEntitas.LevelMobBluePrints));
        component.Collection = newCollection;
        ReplaceComponent(index, component);
    }

    public void RemoveLevelMobBluePrints() {
        RemoveComponent(CoreGamePlayComponentsLookup.LevelMobBluePrints);
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

    static Entitas.IMatcher<CoreGamePlayEntity> _matcherLevelMobBluePrints;

    public static Entitas.IMatcher<CoreGamePlayEntity> LevelMobBluePrints {
        get {
            if (_matcherLevelMobBluePrints == null) {
                var matcher = (Entitas.Matcher<CoreGamePlayEntity>)Entitas.Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayComponentsLookup.LevelMobBluePrints);
                matcher.componentNames = CoreGamePlayComponentsLookup.componentNames;
                _matcherLevelMobBluePrints = matcher;
            }

            return _matcherLevelMobBluePrints;
        }
    }
}