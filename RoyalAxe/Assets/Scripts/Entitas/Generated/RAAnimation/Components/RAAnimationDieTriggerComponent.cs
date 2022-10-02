//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class RAAnimationEntity {

    static readonly RoyalAxe.GameEntitas.DieTriggerComponent dieTriggerComponent = new RoyalAxe.GameEntitas.DieTriggerComponent();

    public bool isDieTrigger {
        get { return HasComponent(RAAnimationComponentsLookup.DieTrigger); }
        set {
            if (value != isDieTrigger) {
                var index = RAAnimationComponentsLookup.DieTrigger;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : dieTriggerComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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
public sealed partial class RAAnimationMatcher {

    static Entitas.IMatcher<RAAnimationEntity> _matcherDieTrigger;

    public static Entitas.IMatcher<RAAnimationEntity> DieTrigger {
        get {
            if (_matcherDieTrigger == null) {
                var matcher = (Entitas.Matcher<RAAnimationEntity>)Entitas.Matcher<RAAnimationEntity>.AllOf(RAAnimationComponentsLookup.DieTrigger);
                matcher.componentNames = RAAnimationComponentsLookup.componentNames;
                _matcherDieTrigger = matcher;
            }

            return _matcherDieTrigger;
        }
    }
}
