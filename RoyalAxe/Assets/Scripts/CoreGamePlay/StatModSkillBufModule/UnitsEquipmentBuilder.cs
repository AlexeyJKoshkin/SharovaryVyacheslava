using RoyalAxe.GameEntitas;

namespace RoyalAxe.Units.Stats 
{
    public class UnitsEquipmentBuilder : IUnitsEquipmentBuilder
    {
        private readonly ISkillFactory _skillFactory;
        private readonly IUnitItemFactory _itemFactory;
        public UnitsEquipmentBuilder(ISkillFactory skillFactory, IUnitItemFactory itemFactory)
        {
            _skillFactory = skillFactory;
            _itemFactory  = itemFactory;
        }

        public void EquipMobWeapon(UnitsEntity unit, WeaponBluePrint weaponBluePrint)
        {
            var skb = weaponBluePrint.SkillBlueprint;
            unit.AddUnitEquipWeaponData( skb.Id, skb.Level,skb.RangeData.MissileSpeed);
            EquipWeaponSkill(unit, weaponBluePrint);
            TryAddDefaultGunnerSkill(skb.RangeData, unit);
        }

        public void EquipPlayer(UnitsEntity player, WeaponBluePrint mainWeapon)
        {
            var skb = mainWeapon.SkillBlueprint;
            player.AddUnitEquipWeaponData(skb.Id, skb.Level,skb.RangeData.MissileSpeed);
            EquipWeaponSkill(player, mainWeapon);
            _skillFactory.CreateWeaponSkill(skb.RangeData, player).isDefaultPlayerSkill = true;
        }

        private void TryAddDefaultGunnerSkill(SkillConfigDef.RangeParams rangeParams, UnitsEntity mob)
        {
            if (rangeParams.RangeCooldownAttack <= 0) return;
            var skill = _skillFactory.CreateRangeSkill(rangeParams, mob);
            skill.AddGunnerMobSkill(mob);
        }
 
        private void EquipWeaponSkill(UnitsEntity unit, WeaponBluePrint weaponBluePrint)
        {
            var mainItem = _itemFactory.CreateMainItem(weaponBluePrint);
            unit.EquipMainItem(mainItem);
        }
    }
}