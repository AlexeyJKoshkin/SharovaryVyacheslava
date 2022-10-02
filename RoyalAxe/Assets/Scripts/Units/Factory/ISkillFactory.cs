namespace RoyalAxe.GameEntitas
{
    public interface ISkillFactory
    {
        void EquipMobWeapon(UnitsEntity unitsEntity, string weaponId, byte weaponLevel);

        void CreateTestPlayerSkill(UnitsEntity player, string weaponId, byte weaponLevel);
        void CreateMeleeAttackSkill(UnitsEntity boson, UnitsEntity owner);
    }
}