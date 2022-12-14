//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UnitsEntity {

    public RoyalAxe.GameEntitas.UnitsViewComponent unitsView { get { return (RoyalAxe.GameEntitas.UnitsViewComponent)GetComponent(UnitsComponentsLookup.UnitsView); } }
    public bool hasUnitsView { get { return HasComponent(UnitsComponentsLookup.UnitsView); } }

    public void AddUnitsView(RoyalAxe.Units.BaseUnitView newView) {
        var index = UnitsComponentsLookup.UnitsView;
        var component = (RoyalAxe.GameEntitas.UnitsViewComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.UnitsViewComponent));
        component.View = newView;
        AddComponent(index, component);
    }

    public void ReplaceUnitsView(RoyalAxe.Units.BaseUnitView newView) {
        var index = UnitsComponentsLookup.UnitsView;
        var component = (RoyalAxe.GameEntitas.UnitsViewComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.UnitsViewComponent));
        component.View = newView;
        ReplaceComponent(index, component);
    }

    public void RemoveUnitsView() {
        RemoveComponent(UnitsComponentsLookup.UnitsView);
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

    static Entitas.IMatcher<UnitsEntity> _matcherUnitsView;

    public static Entitas.IMatcher<UnitsEntity> UnitsView {
        get {
            if (_matcherUnitsView == null) {
                var matcher = (Entitas.Matcher<UnitsEntity>)Entitas.Matcher<UnitsEntity>.AllOf(UnitsComponentsLookup.UnitsView);
                matcher.componentNames = UnitsComponentsLookup.componentNames;
                _matcherUnitsView = matcher;
            }

            return _matcherUnitsView;
        }
    }
}
