//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameRootLoopEntity {

    public RoyalAxe.GameEntitas.AdditionalDataBoxComponent additionalDataBox { get { return (RoyalAxe.GameEntitas.AdditionalDataBoxComponent)GetComponent(GameRootLoopComponentsLookup.AdditionalDataBox); } }
    public bool hasAdditionalDataBox { get { return HasComponent(GameRootLoopComponentsLookup.AdditionalDataBox); } }

    public void AddAdditionalDataBox(Core.Data.Provider.DataBox[] newCollection) {
        var index = GameRootLoopComponentsLookup.AdditionalDataBox;
        var component = (RoyalAxe.GameEntitas.AdditionalDataBoxComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.AdditionalDataBoxComponent));
        component.Collection = newCollection;
        AddComponent(index, component);
    }

    public void ReplaceAdditionalDataBox(Core.Data.Provider.DataBox[] newCollection) {
        var index = GameRootLoopComponentsLookup.AdditionalDataBox;
        var component = (RoyalAxe.GameEntitas.AdditionalDataBoxComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.AdditionalDataBoxComponent));
        component.Collection = newCollection;
        ReplaceComponent(index, component);
    }

    public void RemoveAdditionalDataBox() {
        RemoveComponent(GameRootLoopComponentsLookup.AdditionalDataBox);
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
public sealed partial class GameRootLoopMatcher {

    static Entitas.IMatcher<GameRootLoopEntity> _matcherAdditionalDataBox;

    public static Entitas.IMatcher<GameRootLoopEntity> AdditionalDataBox {
        get {
            if (_matcherAdditionalDataBox == null) {
                var matcher = (Entitas.Matcher<GameRootLoopEntity>)Entitas.Matcher<GameRootLoopEntity>.AllOf(GameRootLoopComponentsLookup.AdditionalDataBox);
                matcher.componentNames = GameRootLoopComponentsLookup.componentNames;
                _matcherAdditionalDataBox = matcher;
            }

            return _matcherAdditionalDataBox;
        }
    }
}
