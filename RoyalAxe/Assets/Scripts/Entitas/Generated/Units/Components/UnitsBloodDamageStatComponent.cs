//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UnitsEntity {

    public RoyalAxe.GameEntitas.BloodDamageStatComponent bloodDamageStat { get { return (RoyalAxe.GameEntitas.BloodDamageStatComponent)GetComponent(UnitsComponentsLookup.BloodDamageStat); } }
    public bool hasBloodDamageStat { get { return HasComponent(UnitsComponentsLookup.BloodDamageStat); } }

    public void AddBloodDamageStat(RoyalAxe.Units.Stats.CharacterStatValue newUnitStatValue) {
        var index = UnitsComponentsLookup.BloodDamageStat;
        var component = (RoyalAxe.GameEntitas.BloodDamageStatComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.BloodDamageStatComponent));
        component.UnitStatValue = newUnitStatValue;
        AddComponent(index, component);
    }

    public void ReplaceBloodDamageStat(RoyalAxe.Units.Stats.CharacterStatValue newUnitStatValue) {
        var index = UnitsComponentsLookup.BloodDamageStat;
        var component = (RoyalAxe.GameEntitas.BloodDamageStatComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.BloodDamageStatComponent));
        component.UnitStatValue = newUnitStatValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBloodDamageStat() {
        RemoveComponent(UnitsComponentsLookup.BloodDamageStat);
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

    static Entitas.IMatcher<UnitsEntity> _matcherBloodDamageStat;

    public static Entitas.IMatcher<UnitsEntity> BloodDamageStat {
        get {
            if (_matcherBloodDamageStat == null) {
                var matcher = (Entitas.Matcher<UnitsEntity>)Entitas.Matcher<UnitsEntity>.AllOf(UnitsComponentsLookup.BloodDamageStat);
                matcher.componentNames = UnitsComponentsLookup.componentNames;
                _matcherBloodDamageStat = matcher;
            }

            return _matcherBloodDamageStat;
        }
    }
}
