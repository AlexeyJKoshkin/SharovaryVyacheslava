using System;
using Entitas;
using RoyalAxe.Units;
using RoyalAxe.Units.Stats;

namespace RoyalAxe.GameEntitas {
    [Skill]
    public class BuffApplierComponent : ListCollectionComponent<IUnitApplierItem> { }

    /// <summary>
    /// Цель бафа. Тот на которого бафф повесили
    /// </summary>
    [Skill]
    public class BuffTargetComponent : IComponent
    {
        public UnitsEntity Target;
    }
    
    //Владелец бафа - тот кто его создал
    [Skill]
    public class BuffOwnerComponent : IComponent
    {
        public Guid Guid;
        public UnitsEntity Owner;
    }

    [Skill]
    public class ElementalDamageBufComponent : IComponent
    {
        public DamageType Type;
    }
    [Skill]
    public class FreezeBuffComponent : IComponent { }

    [Skill]
    public class BuffBehaviourComponent : IComponent
    {
        public IBuffBehavior BehaviourTreeNode;
    }
}