using System.Collections.Generic;
using Core.Data.Provider;
using RoyalAxe.CharacterStat;
using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.GameEntitas
{
    public class SkillFactory : AbstractEntityFactory<SkillEntity, SkillContext>, ISkillFactory
    {
        private readonly ITimerFactory _timerFactory;
        private readonly IUnitItemFactory _unitItemFactory;

        public SkillFactory(SkillContext skillContext,
                            ITimerFactory timerFactory,
                            IUnitItemFactory unitItemFactory) : base(skillContext)
        {
            _timerFactory = timerFactory;
            _unitItemFactory = unitItemFactory;
        }

        public void EquipMobWeapon(UnitsEntity unit, SkillBlueprint skillBlueprint)
        {
            EquipWeaponSkill(unit, skillBlueprint);
            TryAddDefaultGunnerSkill(skillBlueprint.RangeData, unit);
        }

        public void CreateTestPlayerSkill(UnitsEntity player, SkillBlueprint skillBlueprint)
        {
            EquipWeaponSkill(player, skillBlueprint);
            var playerSkill = CreateWeaponSkill(skillBlueprint.RangeData, player);
            playerSkill.isDefaultPlayerSkill = true;
        }

        public void CreateMeleeAttackSkill(UnitsEntity boson, UnitsEntity owner)
        {
            var weaponData = owner.unitEquipWeaponData;

            //var damage = _toSkillUtility.CreateComposite(weaponData.Damage);
            boson.AddMainDamage(owner.mainDamage.Influence);
            if(owner.hasOtherDamage)
                boson.AddOtherDamage(owner.otherDamage.Collection);
            boson.ReplaceMoveSpeed(new CharacterStatValue
            {
                MinValue = 0,
                MaxValue = 100,
                Value = weaponData.Range.MissileSpeed
            });
        }

        private void TryAddDefaultGunnerSkill(SkillConfigDef.RangeParams rangeParams, UnitsEntity mob)
        {
            if (rangeParams.RangeCooldownAttack <= 0) return;

            var skill = CreateWeaponSkill(rangeParams, mob);
            var timer = _timerFactory.CreateRestoreUsageSkillTimer(skill, rangeParams.RangeCooldownAttack);
            skill.AddRestoreAttemptsTimer(1, timer);
            skill.AddGunnerMobSkill(mob);
        }

        private void EquipWeaponSkill(UnitsEntity unit, SkillBlueprint skillBlueprint)
        {
            unit.AddUnitEquipWeaponData(skillBlueprint.DamageData, skillBlueprint.RangeData, skillBlueprint.Id, skillBlueprint.Level);
            var mainItem = _unitItemFactory.CreateMainItem(skillBlueprint.DamageData);
            unit.EquipMainItem(mainItem);
          
        }

        private SkillEntity CreateWeaponSkill(SkillConfigDef.RangeParams rangeParams, UnitsEntity unit)
        {
            var skill = Context.CreateEntity();
            skill.AddUseCounterSkill(rangeParams.StartUsage, rangeParams.StartUsage);
            skill.AddPriceUseSkill(rangeParams.PriceUsage);
            unit.AddUnitActiveSkill(skill);
            return skill;
        }
    }
}
