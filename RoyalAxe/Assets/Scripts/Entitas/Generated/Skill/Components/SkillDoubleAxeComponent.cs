//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class SkillEntity {

    static readonly RoyalAxe.GameEntitas.DoubleAxeComponent doubleAxeComponent = new RoyalAxe.GameEntitas.DoubleAxeComponent();

    public bool isDoubleAxe {
        get { return HasComponent(SkillComponentsLookup.DoubleAxe); }
        set {
            if (value != isDoubleAxe) {
                var index = SkillComponentsLookup.DoubleAxe;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : doubleAxeComponent;

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
public sealed partial class SkillMatcher {

    static Entitas.IMatcher<SkillEntity> _matcherDoubleAxe;

    public static Entitas.IMatcher<SkillEntity> DoubleAxe {
        get {
            if (_matcherDoubleAxe == null) {
                var matcher = (Entitas.Matcher<SkillEntity>)Entitas.Matcher<SkillEntity>.AllOf(SkillComponentsLookup.DoubleAxe);
                matcher.componentNames = SkillComponentsLookup.componentNames;
                _matcherDoubleAxe = matcher;
            }

            return _matcherDoubleAxe;
        }
    }
}
