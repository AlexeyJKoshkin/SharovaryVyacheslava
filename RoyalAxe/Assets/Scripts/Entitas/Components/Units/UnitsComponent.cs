using System;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using RoyalAxe.Units.Stats;
using RoyalAxe.Units.UnitBehaviour;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    [Units]
    public class UnitComponent : IComponent
    {
        public string Id;
        public int Level;
    }

    /// <summary>
    ///     нужен уникальный индентификатор моба
    /// </summary>
    [Units]
    public class UniqueUnitGUID : IComponent
    {
        public Guid Guid;
    }


    [Units] // это дополнительный снаряд - типо фейковый
    public class AdditionalBosonComponent : IComponent
    {
        
    }

    [Units]
    public class ActiveUnitBuffComponent : HashSetCollectionComponent<SkillEntity> { }

    [Units]
    public class PossibleTargetsComponent : ListCollectionComponent<UnitsEntity> { }

 
    [Units]
    public class DeadUnit : IComponent { }

    [Units]
    public class DestroyUnitComponent : IComponent { }

    
    [Units]
    public class UnitActiveSkillComponent : IComponent
    {
        public SkillEntity SkillEntity;
    }

    [Units]
    public class UnitEquipWeaponDataComponent : IComponent
    {
        public string Id;
        public int Level;
        public float MissileSpeed;
    }
}