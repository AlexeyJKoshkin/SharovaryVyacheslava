//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class RAAnimationEntity {

    public RoyalAxe.GameEntitas.IsSitComponent isSit { get { return (RoyalAxe.GameEntitas.IsSitComponent)GetComponent(RAAnimationComponentsLookup.IsSit); } }
    public bool hasIsSit { get { return HasComponent(RAAnimationComponentsLookup.IsSit); } }

    public void AddIsSit(bool newValue) {
        var index = RAAnimationComponentsLookup.IsSit;
        var component = (RoyalAxe.GameEntitas.IsSitComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.IsSitComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceIsSit(bool newValue) {
        var index = RAAnimationComponentsLookup.IsSit;
        var component = (RoyalAxe.GameEntitas.IsSitComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.IsSitComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveIsSit() {
        RemoveComponent(RAAnimationComponentsLookup.IsSit);
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

    static Entitas.IMatcher<RAAnimationEntity> _matcherIsSit;

    public static Entitas.IMatcher<RAAnimationEntity> IsSit {
        get {
            if (_matcherIsSit == null) {
                var matcher = (Entitas.Matcher<RAAnimationEntity>)Entitas.Matcher<RAAnimationEntity>.AllOf(RAAnimationComponentsLookup.IsSit);
                matcher.componentNames = RAAnimationComponentsLookup.componentNames;
                _matcherIsSit = matcher;
            }

            return _matcherIsSit;
        }
    }
}
