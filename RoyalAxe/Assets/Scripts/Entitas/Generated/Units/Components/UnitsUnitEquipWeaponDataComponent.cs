//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class UnitsEntity {

    public RoyalAxe.GameEntitas.UnitEquipWeaponDataComponent unitEquipWeaponData { get { return (RoyalAxe.GameEntitas.UnitEquipWeaponDataComponent)GetComponent(UnitsComponentsLookup.UnitEquipWeaponData); } }
    public bool hasUnitEquipWeaponData { get { return HasComponent(UnitsComponentsLookup.UnitEquipWeaponData); } }

    public void AddUnitEquipWeaponData(float missileSpeed, string newId, int newLevel) {
        var index = UnitsComponentsLookup.UnitEquipWeaponData;
        var component = (RoyalAxe.GameEntitas.UnitEquipWeaponDataComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.UnitEquipWeaponDataComponent));
        component.MissileSpeed = missileSpeed;
      //  component.Range = newRange;
        component.Id = newId;
        component.Level = newLevel;
        AddComponent(index, component);
    }

    public void ReplaceUnitEquipWeaponData(float missileSpeed, string newId, int newLevel) {
        var index = UnitsComponentsLookup.UnitEquipWeaponData;
        var component = (RoyalAxe.GameEntitas.UnitEquipWeaponDataComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.UnitEquipWeaponDataComponent));
        component.MissileSpeed = missileSpeed;
    //    component.Range = newRange;
        component.Id = newId;
        component.Level = newLevel;
        ReplaceComponent(index, component);
    }

    public void RemoveUnitEquipWeaponData() {
        RemoveComponent(UnitsComponentsLookup.UnitEquipWeaponData);
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

    static Entitas.IMatcher<UnitsEntity> _matcherUnitEquipWeaponData;

    public static Entitas.IMatcher<UnitsEntity> UnitEquipWeaponData {
        get {
            if (_matcherUnitEquipWeaponData == null) {
                var matcher = (Entitas.Matcher<UnitsEntity>)Entitas.Matcher<UnitsEntity>.AllOf(UnitsComponentsLookup.UnitEquipWeaponData);
                matcher.componentNames = UnitsComponentsLookup.componentNames;
                _matcherUnitEquipWeaponData = matcher;
            }

            return _matcherUnitEquipWeaponData;
        }
    }
}
