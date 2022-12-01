//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UnitsEntity {

    public RoyalAxe.GameEntitas.MainDamageComponent mainDamage { get { return (RoyalAxe.GameEntitas.MainDamageComponent)GetComponent(UnitsComponentsLookup.MainDamage); } }
    public bool hasMainDamage { get { return HasComponent(UnitsComponentsLookup.MainDamage); } }

    public void AddMainDamage(RoyalAxe.IInfluenceApplierComposite newInfluence) {
        var index = UnitsComponentsLookup.MainDamage;
        var component = (RoyalAxe.GameEntitas.MainDamageComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.MainDamageComponent));
        component.Influence = newInfluence;
        AddComponent(index, component);
    }

    public void ReplaceMainDamage(RoyalAxe.IInfluenceApplierComposite newInfluence) {
        var index = UnitsComponentsLookup.MainDamage;
        var component = (RoyalAxe.GameEntitas.MainDamageComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.MainDamageComponent));
        component.Influence = newInfluence;
        ReplaceComponent(index, component);
    }

    public void RemoveMainDamage() {
        RemoveComponent(UnitsComponentsLookup.MainDamage);
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

    static Entitas.IMatcher<UnitsEntity> _matcherMainDamage;

    public static Entitas.IMatcher<UnitsEntity> MainDamage {
        get {
            if (_matcherMainDamage == null) {
                var matcher = (Entitas.Matcher<UnitsEntity>)Entitas.Matcher<UnitsEntity>.AllOf(UnitsComponentsLookup.MainDamage);
                matcher.componentNames = UnitsComponentsLookup.componentNames;
                _matcherMainDamage = matcher;
            }

            return _matcherMainDamage;
        }
    }
}
