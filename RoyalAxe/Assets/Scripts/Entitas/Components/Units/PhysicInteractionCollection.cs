using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    [DontGenerate]
    public abstract class PhysicInteractionCollection : AbstractCollectionComponent<HashSet<Collider2D>, Collider2D>
    {
        private static readonly HashSet<Collider2D> Empty = new HashSet<Collider2D>();
       // public Dictionary<RoyalAxeTagNames, HashSet<Collider2D>> Collection;
        
        //public HashSet<Collider2D> Collection;
    }

    [Units]
    public class EnterPhysicInteractionComponent : PhysicInteractionCollection { }

    [Units]
    public class ExitPhysicInteractionComponent : PhysicInteractionCollection { }
}