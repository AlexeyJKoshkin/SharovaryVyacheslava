//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UnitsEntity {

    public RoyalAxe.GameEntitas.UnitComponent unit { get { return (RoyalAxe.GameEntitas.UnitComponent)GetComponent(UnitsComponentsLookup.Unit); } }
    public bool hasUnit { get { return HasComponent(UnitsComponentsLookup.Unit); } }

    public void AddUnit(string newId, int newLevel) {
        var index = UnitsComponentsLookup.Unit;
        var component = (RoyalAxe.GameEntitas.UnitComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.UnitComponent));
        component.Id = newId;
        component.Level = newLevel;
        AddComponent(index, component);
    }

    public void ReplaceUnit(string newId, int newLevel) {
        var index = UnitsComponentsLookup.Unit;
        var component = (RoyalAxe.GameEntitas.UnitComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.UnitComponent));
        component.Id = newId;
        component.Level = newLevel;
        ReplaceComponent(index, component);
    }

    public void RemoveUnit() {
        RemoveComponent(UnitsComponentsLookup.Unit);
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

    static Entitas.IMatcher<UnitsEntity> _matcherUnit;

    public static Entitas.IMatcher<UnitsEntity> Unit {
        get {
            if (_matcherUnit == null) {
                var matcher = (Entitas.Matcher<UnitsEntity>)Entitas.Matcher<UnitsEntity>.AllOf(UnitsComponentsLookup.Unit);
                matcher.componentNames = UnitsComponentsLookup.componentNames;
                _matcherUnit = matcher;
            }

            return _matcherUnit;
        }
    }
}
