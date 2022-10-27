using Entitas;
using GameKit;

namespace RoyalAxe.CoreLevel
{
    public interface IResetCoreGameToRetryCommand
    {
        void RestartGameAfterPlayerDearth();
    }

    public class ResetCoreGameToRetryCommand : IResetCoreGameToRetryCommand
    {
        private UnitsContext _unitsContext;

        private readonly IGroup<UnitsEntity> _bosonGroup;
        private readonly IGroup<UnitsEntity> _allMobs;
        
        public ResetCoreGameToRetryCommand(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
            _bosonGroup = _unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.Boson, UnitsMatcher.UnitsView));
            _allMobs = _unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.Mob, UnitsMatcher.Damage));
        }

        public void RestartGameAfterPlayerDearth()
        {
            DestroyAllBosons();
            KillAllTimingDamageFromPlayer();
            ResetPlayerStat();
        }

        private void KillAllTimingDamageFromPlayer()
        {
            foreach (var mob in _allMobs)
            {
                mob.damage.PeriodicDamage.ForEach(d =>
                {
                //    d.Apply();
                });
            }
        }

        private void ResetPlayerStat()
        {
            var player = _unitsContext.playerEntity;
            player.enterPhysicInteraction.Clear();
            player.exitPhysicInteraction.Clear();

        }

        private void DestroyAllBosons()
        {
            foreach (var boson in _bosonGroup)
            {
                boson.isDestroyUnit = true;
            }
        }
    }
}
