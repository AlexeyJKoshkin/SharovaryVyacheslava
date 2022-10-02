using Core.UserProfile;
using RoyalAxe.CoreLevel;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public class UnitsBuilderFacade : IUnitsBuilderFacade
    {
        private readonly IUnitsEntityFactory _entityFactory;
        private readonly IUnitsViewBuilder _unitViewBuilder;
        private ILevelAdapter _levelAdapter;

        public UnitsBuilderFacade(IUnitsViewBuilder unitViewBuilder, IUnitsEntityFactory entityFactory, ILevelAdapter levelAdapter)
        {
            _unitViewBuilder = unitViewBuilder;
            _entityFactory   = entityFactory;
            _levelAdapter    = levelAdapter;
        }

        public UnitsEntity CreateEnemyMobUnit(string id, byte level, Vector2 pos)
        {
            var mob  = _entityFactory.CreateEnemyMobUnit(id, level);
            var view = _unitViewBuilder.BuildMobView(mob, pos);
            return mob;
        }


        public void CreatePlayer(HeroProgressData selectedHero, WeaponProgressData selectedWeapon)
        {
            var player = _entityFactory.CreatePlayer(selectedHero, selectedWeapon);
            _unitViewBuilder.BuildPlayerView(player);
        }
    }
}