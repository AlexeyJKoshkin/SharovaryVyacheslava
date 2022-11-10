using System;
using System.Collections.Generic;
using Core.Data.Provider;
using Core.UserProfile;
using RoyalAxe.CharacterStat;

namespace RoyalAxe.GameEntitas
{
    public class UnitsEntityFactory : AbstractEntityFactory<UnitsEntity, UnitsContext>, IUnitsEntityFactory
    {
        private readonly RAAnimationContext _animationContext;
        private readonly ISkillFactory _skillBuilder;
        private readonly UnitStatsBuilder _unitStatsBuilder;

        public UnitsEntityFactory(UnitsContext units, RAAnimationContext animationContext, IDataStorage dataStorage, ISkillFactory skillBuilder) : base(units)
        {
            _animationContext = animationContext;
            _skillBuilder     = skillBuilder;
            _unitStatsBuilder = new UnitStatsBuilder(dataStorage);
        }

        public UnitsEntity CreatePlayer(HeroProgressData characterConfigUniqueId, WeaponProgressData selectedWeapon)
        {
            var player = CreateBlankUnit();
            player.AddUnit(characterConfigUniqueId.CharacterId, characterConfigUniqueId.Level);
            player.isPlayer = true;
            _unitStatsBuilder.AddStats(player, characterConfigUniqueId.CharacterId);
            _skillBuilder.CreateTestPlayerSkill(player, selectedWeapon.WeaponID, selectedWeapon.Level);
            return player;
        }

        public UnitsEntity CreateWizardUnit()
        {
            var entity = Context.CreateEntity();
            entity.AddMoveSpeed(new CharacterStatValue(){Value = 3, MaxValue = 10});
            entity.AddMovingToPoint(new FollowUnitPointAdapter(Context.playerEntity));
            return entity;
        }

        public UnitsEntity CreateEnemyMobUnit(string id, byte level = 1)
        {
            var mob = CreateUnit(id, level);
            mob.AddUnit(id, level);
            mob.isMob = true;
            EquipWeapon(mob, id, level);
            return mob;
        }

        private void EquipWeapon(UnitsEntity entity, string id, byte level)
        {
            _skillBuilder.EquipMobWeapon(entity, id, level);
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


        private UnitsEntity CreateUnit(string unitId, byte level = 1)
        {
            var entity = CreateBlankUnit();
            _unitStatsBuilder.AddStats(entity, unitId, level);
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