using Core.UserProfile;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace RoyalAxe.GameEntitas
{
    [GameRootLoop, Unique]
    public class UserProgressComponent : IComponent
    {
        public UserProfileData ProfileData;
    }

    [DontGenerate]
    public abstract class UserProgressComponent<T>: IComponent
    {
        public T Progress;
    }

    [GameRootLoop]
    public class UserCurrentHeroProgressComponent : UserProgressComponent<HeroProgressData>
    {
    }
    
    [GameRootLoop]
    public class UserCurrentWeaponProgressComponent : UserProgressComponent<WeaponProgressData>
    {
    }
    
    [GameRootLoop]
    public class UserCurrentLevelsProgressComponent : UserProgressComponent<LastLevel>
    {
    }
}
