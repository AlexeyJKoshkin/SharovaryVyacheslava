//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class CoreGamePlayEntity {

    public RoyalAxe.GameEntitas.ChunkBoundsComponent chunkBounds { get { return (RoyalAxe.GameEntitas.ChunkBoundsComponent)GetComponent(CoreGamePlayComponentsLookup.ChunkBounds); } }
    public bool hasChunkBounds { get { return HasComponent(CoreGamePlayComponentsLookup.ChunkBounds); } }

    public void AddChunkBounds(UnityEngine.Bounds newBounds) {
        var index = CoreGamePlayComponentsLookup.ChunkBounds;
        var component = (RoyalAxe.GameEntitas.ChunkBoundsComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.ChunkBoundsComponent));
        component.Bounds = newBounds;
        AddComponent(index, component);
    }

    public void ReplaceChunkBounds(UnityEngine.Bounds newBounds) {
        var index = CoreGamePlayComponentsLookup.ChunkBounds;
        var component = (RoyalAxe.GameEntitas.ChunkBoundsComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.ChunkBoundsComponent));
        component.Bounds = newBounds;
        ReplaceComponent(index, component);
    }

    public void RemoveChunkBounds() {
        RemoveComponent(CoreGamePlayComponentsLookup.ChunkBounds);
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
public sealed partial class CoreGamePlayMatcher {

    static Entitas.IMatcher<CoreGamePlayEntity> _matcherChunkBounds;

    public static Entitas.IMatcher<CoreGamePlayEntity> ChunkBounds {
        get {
            if (_matcherChunkBounds == null) {
                var matcher = (Entitas.Matcher<CoreGamePlayEntity>)Entitas.Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayComponentsLookup.ChunkBounds);
                matcher.componentNames = CoreGamePlayComponentsLookup.componentNames;
                _matcherChunkBounds = matcher;
            }

            return _matcherChunkBounds;
        }
    }
}
