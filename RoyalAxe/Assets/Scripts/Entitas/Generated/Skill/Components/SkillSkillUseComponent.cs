//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class SkillEntity {

    static readonly RoyalAxe.GameEntitas.SkillUseComponent skillUseComponent = new RoyalAxe.GameEntitas.SkillUseComponent();

    public bool isSkillUse {
        get { return HasComponent(SkillComponentsLookup.SkillUse); }
        set {
            if (value != isSkillUse) {
                var index = SkillComponentsLookup.SkillUse;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : skillUseComponent;

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

    static Entitas.IMatcher<SkillEntity> _matcherSkillUse;

    public static Entitas.IMatcher<SkillEntity> SkillUse {
        get {
            if (_matcherSkillUse == null) {
                var matcher = (Entitas.Matcher<SkillEntity>)Entitas.Matcher<SkillEntity>.AllOf(SkillComponentsLookup.SkillUse);
                matcher.componentNames = SkillComponentsLookup.componentNames;
                _matcherSkillUse = matcher;
            }

            return _matcherSkillUse;
        }
    }
}
