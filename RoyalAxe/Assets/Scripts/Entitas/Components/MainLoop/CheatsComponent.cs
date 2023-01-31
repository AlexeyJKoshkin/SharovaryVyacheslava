using Core.UserProfile;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using RoyalAxe.CoreLevel;

namespace RoyalAxe.GameEntitas 
{
    [GameRootLoop, Unique]
    public class CheatsComponent : IComponent
    {
    }

    [GameRootLoop]
    public class CheatStartLevelComponent : IComponent
    {
        public LastLevel Level;
    }

    [GameRootLoop]
    public class HeroStartLevelComponent : HeroProgressData, IComponent
    {
    }
    
    [GameRootLoop]
    public class HeroStartWeaponComponent : WeaponProgressData, IComponent
    {
    }

}