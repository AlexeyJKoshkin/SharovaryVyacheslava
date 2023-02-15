using System;
using System.Collections.Generic;
using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.Units.Stats;
using RoyalAxe.Configs;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public class UnitsEntityFactory : AbstractEntityFactory<UnitsEntity, UnitsContext>, IUnitsEntityFactory
    {
        private readonly RAAnimationContext _animationContext;
        private readonly IUnitsEquipmentBuilder _skillBuilder;
        private readonly UnitStatsBuilder _unitStatsBuilder;

        public UnitsEntityFactory(UnitsContext units, 
                                  RAAnimationContext animationContext,
                                  IDataStorage storage,
                                  IUnitsEquipmentBuilder skillBuilder) : base(units)
        {
            _animationContext = animationContext;
            _skillBuilder     = skillBuilder;
            _unitStatsBuilder = new UnitStatsBuilder(storage);
        }


        public UnitsEntity CreatePlayer(UnitBlueprint unitBlueprint)
        {
            var player = CreateUnit(unitBlueprint);
            player.isPlayer = true;
            _skillBuilder.EquipPlayer(player, unitBlueprint.MainItemBluePrint);
            return player;
        }

        public UnitsEntity CreateWizardUnit()
        {
            var entity = Context.CreateEntity();
            entity.AddMoveSpeed(new CharacterStatValue() {Value = 3, MaxValue = 10});
            entity.AddMovingToPoint(new FollowUnitPointAdapter(Context.playerEntity));
            return entity;
        }

        public UnitsEntity CreateEnemyMobUnit(MobBlueprint mobBlueprint)
        {
            var mob = CreateUnit(mobBlueprint);
            mob.isMob = true;
            _skillBuilder.EquipMobWeapon(mob, mobBlueprint.MainItemBluePrint);
            return mob;
        }

        public UnitsEntity CreateEnemyMobBoson(UnitsEntity owner)
        {
            var entity = CreateMeleeBoson(owner);
            entity.isMob   = true;
            entity.isBoson = true;
            return entity;
        }

        public UnitsEntity CreatePlayerBoson(UnitsEntity owner)
        {
            var entity = CreateMeleeBoson(owner);
            entity.AddPlayerBoson(0);
            entity.isBoson = true;
            return entity;
        }


        private UnitsEntity CreateMeleeBoson(UnitsEntity owner)
        {
            var boson = CreateBlankUnit();
            _unitStatsBuilder.CopyStats(boson, owner);

            boson.AddUnit(owner.unit.Id + "boson", owner.unit.Level);
            CreateMeleeAttackSkill(boson, owner);
            return boson;
        }

        private void CreateMeleeAttackSkill(UnitsEntity boson, UnitsEntity owner)
        {
            var weaponData = owner.unitEquipWeaponData;

            boson.AddMainDamage(owner.mainDamage.Influence);
            if (owner.hasOtherDamage)
                boson.AddOtherDamage(owner.otherDamage.Collection);
            boson.ReplaceMoveSpeed(new CharacterStatValue
            {
                MinValue = 0,
                MaxValue = 100,
                Value    = weaponData.MissileSpeed
            });
        }


        private UnitsEntity CreateUnit(UnitBlueprint blueprint)
        {
            var entity = CreateBlankUnit();
            entity.AddUnit(blueprint.Id, blueprint.Level);
            _unitStatsBuilder.SetStats(entity, blueprint.Stats);

            return entity;
        }

        private UnitsEntity CreateBlankUnit()
        {
            var entity = Context.CreateEntity();
            entity.AddActiveUnitBuff(new HashSet<SkillEntity>());
            entity.AddUniqueUnitGUID(Guid.NewGuid());
            entity.AddPossibleTargets(new List<UnitsEntity>());
            entity.AddUnitAnimationEntity(_animationContext.CreateEntity());

            return entity;
        }
    }
}