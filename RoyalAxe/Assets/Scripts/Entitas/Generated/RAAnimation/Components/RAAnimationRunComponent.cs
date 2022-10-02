//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class RAAnimationEntity {

    public RoyalAxe.GameEntitas.RunComponent run { get { return (RoyalAxe.GameEntitas.RunComponent)GetComponent(RAAnimationComponentsLookup.Run); } }
    public bool hasRun { get { return HasComponent(RAAnimationComponentsLookup.Run); } }

    public void AddRun(bool newValue) {
        var index = RAAnimationComponentsLookup.Run;
        var component = (RoyalAxe.GameEntitas.RunComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.RunComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceRun(bool newValue) {
        var index = RAAnimationComponentsLookup.Run;
        var component = (RoyalAxe.GameEntitas.RunComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.RunComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveRun() {
        RemoveComponent(RAAnimationComponentsLookup.Run);
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

    static Entitas.IMatcher<RAAnimationEntity> _matcherRun;

    public static Entitas.IMatcher<RAAnimationEntity> Run {
        get {
            if (_matcherRun == null) {
                var matcher = (Entitas.Matcher<RAAnimationEntity>)Entitas.Matcher<RAAnimationEntity>.AllOf(RAAnimationComponentsLookup.Run);
                matcher.componentNames = RAAnimationComponentsLookup.componentNames;
                _matcherRun = matcher;
            }

            return _matcherRun;
        }
    }
}
