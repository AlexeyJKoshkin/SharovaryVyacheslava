namespace RoyalAxe.GameEntitas
{
    public class UnitsBuilderFacade : IUnitsBuilderFacade
    {
        private readonly IUnitsEntityFactory _entityFactory;
        private readonly IUnitsViewBuilder _unitViewBuilder;

        public UnitsBuilderFacade(IUnitsViewBuilder unitViewBuilder, IUnitsEntityFactory entityFactory)
        {
            _unitViewBuilder = unitViewBuilder;
            _entityFactory   = entityFactory;
        }


        public UnitsEntity CreateEnemyMobUnit(MobBlueprint mobBlueprint)
        {
            var mob = _entityFactory.CreateEnemyMobUnit(mobBlueprint);
            _unitViewBuilder.BuildMobView(mob, mobBlueprint.Position);
            return mob;
        }

        public void CreatePlayer(UnitBlueprint unitBlueprint)
        {
            var player = _entityFactory.CreatePlayer(unitBlueprint);
            _unitViewBuilder.BuildPlayerView(player);
        }

        public UnitsEntity CreateWizardShowUnit()
        {
            var wizardShop = _entityFactory.CreateWizardUnit();
            _unitViewBuilder.BuildWizardView(wizardShop);
            return wizardShop;
        }
    }
}