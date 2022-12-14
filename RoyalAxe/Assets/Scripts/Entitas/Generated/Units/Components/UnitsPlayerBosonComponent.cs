//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UnitsEntity {

    public RoyalAxe.GameEntitas.PlayerBosonComponent playerBoson { get { return (RoyalAxe.GameEntitas.PlayerBosonComponent)GetComponent(UnitsComponentsLookup.PlayerBoson); } }
    public bool hasPlayerBoson { get { return HasComponent(UnitsComponentsLookup.PlayerBoson); } }

    public void AddPlayerBoson(int newCountInteraction) {
        var index = UnitsComponentsLookup.PlayerBoson;
        var component = (RoyalAxe.GameEntitas.PlayerBosonComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.PlayerBosonComponent));
        component.CountInteraction = newCountInteraction;
        AddComponent(index, component);
    }

    public void ReplacePlayerBoson(int newCountInteraction) {
        var index = UnitsComponentsLookup.PlayerBoson;
        var component = (RoyalAxe.GameEntitas.PlayerBosonComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.PlayerBosonComponent));
        component.CountInteraction = newCountInteraction;
        ReplaceComponent(index, component);
    }

    public void RemovePlayerBoson() {
        RemoveComponent(UnitsComponentsLookup.PlayerBoson);
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
public sealed partial class UnitsMatcher {

    static Entitas.IMatcher<UnitsEntity> _matcherPlayerBoson;

    public static Entitas.IMatcher<UnitsEntity> PlayerBoson {
        get {
            if (_matcherPlayerBoson == null) {
                var matcher = (Entitas.Matcher<UnitsEntity>)Entitas.Matcher<UnitsEntity>.AllOf(UnitsComponentsLookup.PlayerBoson);
                matcher.componentNames = UnitsComponentsLookup.componentNames;
                _matcherPlayerBoson = matcher;
            }

            return _matcherPlayerBoson;
        }
    }
}
