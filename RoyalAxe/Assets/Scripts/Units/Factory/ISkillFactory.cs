namespace RoyalAxe.GameEntitas
{
    public interface ISkillFactory
    {
        void EquipMobWeapon(UnitsEntity unitsEntity, string weaponId, int weaponLevel);

        void CreateTestPlayerSkill(UnitsEntity player, string weaponId, int weaponLevel);
        void CreateMeleeAttackSkill(UnitsEntity boson, UnitsEntity owner);
    }
}