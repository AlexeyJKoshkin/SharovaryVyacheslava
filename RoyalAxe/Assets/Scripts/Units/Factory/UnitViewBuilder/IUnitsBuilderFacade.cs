namespace RoyalAxe.GameEntitas
{
    public interface IUnitsBuilderFacade
    {
        UnitsEntity CreateEnemyMobUnit(MobBlueprint mobBlueprint);

        void CreatePlayer(UnitBlueprint unitBlueprint);

        UnitsEntity CreateWizardShowUnit();
    }
}