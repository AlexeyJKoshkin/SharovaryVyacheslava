using Core;
using Entitas;
using GameKit;

namespace RoyalAxe.CoreLevel
{
    public interface ICoreGameUtility
    {
        void ClearAllBeforeLeaveCoreScene();
    }

    public class CoreGameUtility : ICoreGameUtility
    {
        private readonly Contexts _contexts;
        public CoreGameUtility(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void ClearAllBeforeLeaveCoreScene()
        {
            HLogger.LogError("On Exit Core State");
            ClearContext(_contexts.units);
            ClearContext(_contexts.skill);
            ClearContext(_contexts.rAAnimation);
            ClearContext(_contexts.coreGamePlay);
            ClearTimers(_contexts.gameRootLoop);
        }

        private void ClearTimers(GameRootLoopContext contextsGameRootLoop)
        {
            var timers = contextsGameRootLoop.GetGroup(GameRootLoopMatcher.Timer);
            timers.AsEnumerable().ForEach(e=> e.Destroy());
        }

        private void ClearContext(IContext contextUnits)
        {
            contextUnits.ClearComponentPools();
            contextUnits.Reset();
        }
    }
}
