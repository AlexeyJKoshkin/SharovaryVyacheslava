using System.Linq;
using Entitas;
using GameKit;
using RoyalAxe.Units.Stats;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CoreLevel
{
    public interface IResetCoreGameToRetryCommand
    {
        void RestartGameAfterPlayerDearth();
    }

    public class ResetCoreGameToRetryCommand : IResetCoreGameToRetryCommand
    {
        private readonly UnitsContext _unitsContext;

        private readonly IGroup<UnitsEntity> _bosonGroup;
        
        public ResetCoreGameToRetryCommand(UnitsContext unitsContext)
        {
            _unitsContext = unitsContext;
            _bosonGroup = _unitsContext.GetGroup(Matcher<UnitsEntity>.AllOf(UnitsMatcher.Boson, UnitsMatcher.UnitsView));

        }

        public void RestartGameAfterPlayerDearth()
        {
            DestroyAllBosons();
            ResetPlayerStat();
        }


        private void ResetPlayerStat()
        {
            var player = _unitsContext.playerEntity;
            player.enterPhysicInteraction.Clear();
        //    player.exitPhysicInteraction.Clear();

            var maxHealthValue = player.health.MaxValue;   
            player.health.ChangeValue(maxHealthValue - player.health.CurrentValue);
            player.ReplaceComponent(UnitsComponentsLookup.Health, player.health);

            var buffs = player.activeUnitBuff.Collection;
            var tickDamages = buffs.Where(o => o.hasElementalDamageBuf).ToArray();
            tickDamages.ForEach(e=>
                                {
                                    player.RemoveBuf(e);
                                    buffs.Remove(e);
                                });
            player.ReplaceActiveUnitBuff(buffs);
            player.isDeadUnit = false;
            player.isDestroyUnit = false;


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
