//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class RAAnimationEntity {

    static readonly RoyalAxe.GameEntitas.AttackTriggerComponent attackTriggerComponent = new RoyalAxe.GameEntitas.AttackTriggerComponent();

    public bool isAttackTrigger {
        get { return HasComponent(RAAnimationComponentsLookup.AttackTrigger); }
        set {
            if (value != isAttackTrigger) {
                var index = RAAnimationComponentsLookup.AttackTrigger;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : attackTriggerComponent;

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

    static Entitas.IMatcher<RAAnimationEntity> _matcherAttackTrigger;

    public static Entitas.IMatcher<RAAnimationEntity> AttackTrigger {
        get {
            if (_matcherAttackTrigger == null) {
                var matcher = (Entitas.Matcher<RAAnimationEntity>)Entitas.Matcher<RAAnimationEntity>.AllOf(RAAnimationComponentsLookup.AttackTrigger);
                matcher.componentNames = RAAnimationComponentsLookup.componentNames;
                _matcherAttackTrigger = matcher;
            }

            return _matcherAttackTrigger;
        }
    }
}
