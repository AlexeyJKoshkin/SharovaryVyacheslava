//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UnitsEntity {

    public RoyalAxe.GameEntitas.OtherDamageComponent otherDamage { get { return (RoyalAxe.GameEntitas.OtherDamageComponent)GetComponent(UnitsComponentsLookup.OtherDamage); } }
    public bool hasOtherDamage { get { return HasComponent(UnitsComponentsLookup.OtherDamage); } }

    public void AddOtherDamage(System.Collections.Generic.List<RoyalAxe.GameEntitas.IWeaponItem> newCollection) {
        var index = UnitsComponentsLookup.OtherDamage;
        var component = (RoyalAxe.GameEntitas.OtherDamageComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.OtherDamageComponent));
        component.Collection = newCollection;
        AddComponent(index, component);
    }

    public void ReplaceOtherDamage(System.Collections.Generic.List<RoyalAxe.GameEntitas.IWeaponItem> newCollection) {
        var index = UnitsComponentsLookup.OtherDamage;
        var component = (RoyalAxe.GameEntitas.OtherDamageComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.OtherDamageComponent));
        component.Collection = newCollection;
        ReplaceComponent(index, component);
    }

    public void RemoveOtherDamage() {
        RemoveComponent(UnitsComponentsLookup.OtherDamage);
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

    static Entitas.IMatcher<UnitsEntity> _matcherOtherDamage;

    public static Entitas.IMatcher<UnitsEntity> OtherDamage {
        get {
            if (_matcherOtherDamage == null) {
                var matcher = (Entitas.Matcher<UnitsEntity>)Entitas.Matcher<UnitsEntity>.AllOf(UnitsComponentsLookup.OtherDamage);
                matcher.componentNames = UnitsComponentsLookup.componentNames;
                _matcherOtherDamage = matcher;
            }

            return _matcherOtherDamage;
        }
    }
}
