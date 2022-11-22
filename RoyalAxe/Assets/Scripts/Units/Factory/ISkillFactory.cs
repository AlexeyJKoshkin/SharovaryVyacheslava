namespace RoyalAxe.GameEntitas
{
    public interface ISkillFactory
    {
        void EquipMobWeapon(UnitsEntity unit, SkillBlueprint skillBlueprint);

        void CreateTestPlayerSkill(UnitsEntity player, SkillBlueprint skillBlueprint);
        void CreateMeleeAttackSkill(UnitsEntity boson, UnitsEntity owner);
    }
}