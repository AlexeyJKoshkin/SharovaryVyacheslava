using System;
using System.Collections.Generic;
using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.CharacterStat;
using RoyalAxe.Configs;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public class UnitsEntityFactory : AbstractEntityFactory<UnitsEntity, UnitsContext>, IUnitsEntityFactory
    {
        private readonly RAAnimationContext _animationContext;
        private readonly ISkillFactory _skillBuilder;
        private readonly UnitStatsBuilder _unitStatsBuilder;

        public UnitsEntityFactory(UnitsContext units, RAAnimationContext animationContext, ISkillFactory skillBuilder) : base(units)
        {
            _animationContext = animationContext;
            _skillBuilder     = skillBuilder;
            _unitStatsBuilder = new UnitStatsBuilder();
        }


        public UnitsEntity CreatePlayer(UnitBlueprint unitBlueprint)
        {
            var player     = CreateUnit(unitBlueprint);
            player.isPlayer = true;
            _skillBuilder.CreateTestPlayerSkill(player, unitBlueprint.ActiveSkill);
            return player;
        }

        public UnitsEntity CreateWizardUnit()
        {
            var entity = Context.CreateEntity();
            entity.AddMoveSpeed(new CharacterStatValue(){Value = 3, MaxValue = 10});
            entity.AddMovingToPoint(new FollowUnitPointAdapter(Context.playerEntity));
            return entity;
        }

        public UnitsEntity CreateEnemyMobUnit(MobBlueprint mobBlueprint)
        {
            var mob = CreateUnit(mobBlueprint);
            mob.isMob = true;
            _skillBuilder.EquipMobWeapon(mob, mobBlueprint.ActiveSkill);
            return mob;
        }

        public UnitsEntity CreateEnemyMobBoson(UnitsEntity owner)
        {
            var entity = CreateMeleeBoson(owner);
            entity.isMob                              = true;
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
            var entity = CreateBlankUnit();
            _unitStatsBuilder.CopyStats(entity, owner);
            entity.AddUnit(owner.unit.Id + "boson", owner.unit.Level);
            _skillBuilder.CreateMeleeAttackSkill(entity, owner);
            return entity;
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
            entity.AddActiveUnitBuff(new HashSet<IEntityBuff>());
            entity.AddUniqueUnitGUID(Guid.NewGuid());
            entity.AddPossibleTargets(new List<UnitsEntity>());
            entity.AddUnitAnimationEntity(_animationContext.CreateEntity());
            return entity;
        }
    }

  
}