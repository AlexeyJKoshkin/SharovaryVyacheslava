//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameRootLoopEntity {

    public RoyalAxe.GameEntitas.UserCurrentHeroProgressComponent userCurrentHeroProgress { get { return (RoyalAxe.GameEntitas.UserCurrentHeroProgressComponent)GetComponent(GameRootLoopComponentsLookup.UserCurrentHeroProgress); } }
    public bool hasUserCurrentHeroProgress { get { return HasComponent(GameRootLoopComponentsLookup.UserCurrentHeroProgress); } }

    public void AddUserCurrentHeroProgress(Core.UserProfile.HeroProgressData newProgress) {
        var index = GameRootLoopComponentsLookup.UserCurrentHeroProgress;
        var component = (RoyalAxe.GameEntitas.UserCurrentHeroProgressComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.UserCurrentHeroProgressComponent));
        component.Progress = newProgress;
        AddComponent(index, component);
    }

    public void ReplaceUserCurrentHeroProgress(Core.UserProfile.HeroProgressData newProgress) {
        var index = GameRootLoopComponentsLookup.UserCurrentHeroProgress;
        var component = (RoyalAxe.GameEntitas.UserCurrentHeroProgressComponent)CreateComponent(index, typeof(RoyalAxe.GameEntitas.UserCurrentHeroProgressComponent));
        component.Progress = newProgress;
        ReplaceComponent(index, component);
    }

    public void RemoveUserCurrentHeroProgress() {
        RemoveComponent(GameRootLoopComponentsLookup.UserCurrentHeroProgress);
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

    static Entitas.IMatcher<GameRootLoopEntity> _matcherUserCurrentHeroProgress;

    public static Entitas.IMatcher<GameRootLoopEntity> UserCurrentHeroProgress {
        get {
            if (_matcherUserCurrentHeroProgress == null) {
                var matcher = (Entitas.Matcher<GameRootLoopEntity>)Entitas.Matcher<GameRootLoopEntity>.AllOf(GameRootLoopComponentsLookup.UserCurrentHeroProgress);
                matcher.componentNames = GameRootLoopComponentsLookup.componentNames;
                _matcherUserCurrentHeroProgress = matcher;
            }

            return _matcherUserCurrentHeroProgress;
        }
    }
}
