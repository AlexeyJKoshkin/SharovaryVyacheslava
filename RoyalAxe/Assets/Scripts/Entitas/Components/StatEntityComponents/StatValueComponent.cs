using Entitas.CodeGeneration.Attributes;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    [Units]
    [Event(EventTarget.Self)]
    public class HealthComponent : ModifiableStat { }

    [Units]
    public class MoveSpeedComponent : ModifiableStat { }

    [Units]
    public class PhysicalDamageStatComponent : ModifiableStat { }

    [Units]
    public class FireDamageStatComponent : ModifiableStat { }
    
    [Units]
    public class ColdDamageStatComponent : ModifiableStat { }
    
    [Units]
    public class PoisonDamageStatComponent : ModifiableStat { }

    [Units]
    public class AttackSpeedComponent : ModifiableStat { }
}