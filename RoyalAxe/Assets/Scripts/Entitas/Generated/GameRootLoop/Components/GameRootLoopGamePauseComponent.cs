//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameRootLoopContext {

    public GameRootLoopEntity gamePauseEntity { get { return GetGroup(GameRootLoopMatcher.GamePause).GetSingleEntity(); } }
    public RoyalAxe.GameEntitas.GamePauseComponent gamePause { get { return gamePauseEntity.gamePause; } }
    public bool hasGamePause { get { return gamePauseEntity != null; } }

    public GameRootLoopEntity SetGamePause(bool newIsPause) {
        if (hasGamePause) {
            throw new Entitas.EntitasException("Could not set GamePause!\n" + this + " already has an entity with RoyalAxe.GameEntitas.GamePauseComponent!",
                "You should check if the context already has a gamePauseEntity before setting it or use context.ReplaceGamePause().");
        }
        var entity = CreateEntity();
        entity.AddGamePause(newIsPause);
        return entity;
    }

    public void ReplaceGamePause(bool newIsPause) {
        var entity = gamePauseEntity;
        if (entity == null) {
            entity = SetGamePause(newIsPause);
        } else {
            entity.ReplaceGamePause(newIsPause);
        }
    }

    public void RemoveGamePause() {
        gamePauseEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameRootLoopEntity {

    public RoyalAxe.GameEntitas.GamePauseComponent gamePause { get { return (RoyalAxe.GameEntitas.GamePauseComponent)GetComponent(GameRootLoopComponentsLookup.GamePause); } }
    public bool hasGamePause { get { return HasComponent(GameRootLoopComponentsLookup.GamePause); } }

    public void AddGamePause(bool newIsPause) {
        var index = GameRootLoopComponentsLookup.GamePause;
        var component = (RoyalAxe.GameEntitas.GamePauseComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.GamePauseComponent));
        component.IsPause = newIsPause;
        AddComponent(index, component);
    }

    public void ReplaceGamePause(bool newIsPause) {
        var index = GameRootLoopComponentsLookup.GamePause;
        var component = (RoyalAxe.GameEntitas.GamePauseComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.GamePauseComponent));
        component.IsPause = newIsPause;
        ReplaceComponent(index, component);
    }

    public void RemoveGamePause() {
        RemoveComponent(GameRootLoopComponentsLookup.GamePause);
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

    static Entitas.IMatcher<GameRootLoopEntity> _matcherGamePause;

    public static Entitas.IMatcher<GameRootLoopEntity> GamePause {
        get {
            if (_matcherGamePause == null) {
                var matcher = (Entitas.Matcher<GameRootLoopEntity>)Entitas.Matcher<GameRootLoopEntity>.AllOf(GameRootLoopComponentsLookup.GamePause);
                matcher.componentNames = GameRootLoopComponentsLookup.componentNames;
                _matcherGamePause = matcher;
            }

            return _matcherGamePause;
        }
    }
}
