using System.Collections.Generic;
using Core.Data.Provider;
using RoyalAxe.CharacterStat;
using RoyalAxe.EntitasSystems.TimerUtility;

namespace RoyalAxe.GameEntitas
{
    public class SkillFactory : AbstractEntityFactory<SkillEntity, SkillContext>, ISkillFactory
    {
        private readonly ITimerFactory _timerFactory;
        private readonly IDataStorage _storage;

        public SkillFactory(SkillContext skillContext,
                            ITimerFactory timerFactory,
                            IDataStorage storage) : base(skillContext)
        {
            _timerFactory = timerFactory;
            _storage      = storage;
        }

        public void EquipMobWeapon(UnitsEntity unit, string weaponId, byte weaponLevel)
        {
            var rangeData = EquipWeaponSkill(unit, weaponId, weaponLevel);
            TryAddDefaultGunnerSkill(rangeData, unit);
        }

        public void CreateTestPlayerSkill(UnitsEntity player, string weaponId, byte weaponLevel)
        {
            var range       = EquipWeaponSkill(player, weaponId, weaponLevel);
            var playerSkill = CreateWeaponSkill(range, player);
            playerSkill.isDefaultPlayerSkill = true;
        }

        public void CreateMeleeAttackSkill(UnitsEntity boson, UnitsEntity owner)
        {
            var weaponData = owner.unitEquipWeaponData;

            AddDamageComponent(boson, weaponData.Damage);

            boson.ReplaceMoveSpeed(new CharacterStatValue
            {
                MinValue = 0,
                MaxValue = 100,
                Value    = weaponData.Range.MissileSpeed
            });
        }

        private void TryAddDefaultGunnerSkill(SkillConfigDef.RangeParams rangeParams, UnitsEntity mob)
        {
            if (rangeParams.RangeCooldownAttack <= 0)
            {
                return;
            }

            var skill = CreateWeaponSkill(rangeParams, mob);
            var timer = _timerFactory.CreateRestoreUsageSkillTimer(skill, rangeParams.RangeCooldownAttack);
            skill.AddRestoreAttemptsTimer(1, timer);
            skill.AddGunnerMobSkill(mob);
        }

        private SkillConfigDef.RangeParams EquipWeaponSkill(UnitsEntity unit, string weaponId, byte level)
        {
            var weaponData = _storage.ById<WeaponsSkillConfigDef>(weaponId).GetByLevel(level);
            unit.AddUnitEquipWeaponData(weaponData.damage, weaponData.rangeParams, weaponId, level);
            AddDamageComponent(unit, weaponData.damage);
            return weaponData.rangeParams;
        }

        private SkillEntity CreateWeaponSkill(SkillConfigDef.RangeParams rangeParams, UnitsEntity unit)
        {
            var skill = Context.CreateEntity();
            skill.AddUseCounterSkill(rangeParams.StartUsage, rangeParams.StartUsage);
            skill.AddPriceUseSkill(rangeParams.PriceUsage);
            unit.AddUnitActiveSkill(skill);
            return skill;
        }


        private void AddDamageComponent(UnitsEntity unit, SkillConfigDef.Damage damage)
        {
            var list = new List<OneMomentDamageOperation>();

            ElementalDamageBuf.ElementalBufApplyHelper periodicDamage = null;


            list.Add(new OneMomentDamageOperation(DamageType.Physical, damage.PhysicalDamage));

            if (damage.ElementalDamage > 0 && damage.DamageCooldown <= 0) // есть одномоментный магический урон
            {
                list.Add(new OneMomentDamageOperation(damage.ElementalDamageType, damage.ElementalDamage));
            }

            if (damage.ElementalDamage > 0 && damage.DamageCooldown > 0) // есть елементальный урон размазанный по времени
            {
                var periodicDamageInfluenceData = new PeriodicDamageInfluenceData
                {
                    MagicDuration       = damage.MagicDuration,
                    DamageCooldown      = damage.DamageCooldown,
                    Damage              = damage.ElementalDamage,
                    ElementalDamageType = damage.ElementalDamageType
                };

                periodicDamage = new ElementalDamageBuf.ElementalBufApplyHelper(periodicDamageInfluenceData);
            }

            var periodic = periodicDamage == null
                ? new List<ElementalDamageBuf.ElementalBufApplyHelper>()
                : new List<ElementalDamageBuf.ElementalBufApplyHelper> {periodicDamage};

            unit.AddDamage(list, periodic);
        }
    }
}