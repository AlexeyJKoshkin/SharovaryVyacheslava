using Core.UserProfile;
using RoyalAxe.Units;
using UnityEngine;

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

        public UnitsEntity CreateEnemyMobUnit(string id, byte level, Vector2 pos)
        {
            var mob  = _entityFactory.CreateEnemyMobUnit(id, level);
            _unitViewBuilder.BuildMobView(mob, pos);
            return mob;
        }


        public void CreatePlayer(HeroProgressData selectedHero, WeaponProgressData selectedWeapon)
        {
            var player = _entityFactory.CreatePlayer(selectedHero, selectedWeapon);
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