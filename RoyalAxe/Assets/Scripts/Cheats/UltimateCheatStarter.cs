using VContainer.Unity;

namespace RoyalAxe.CoreLevel 
{
    public interface IUltimateCheatAdapter
    {
        bool EnableCheats { get; }
        GameRootLoopEntity Cheats { get; }
        bool EnableRender { get; }
    }


    public class UltimateCheatStarter : IUltimateCheatAdapter, IInitializable
    {
        private readonly UltimateCheatSettings _cheatSettings;
        private readonly GameRootLoopContext _rootLoopContext;
        private GameRootLoopEntity _cheatEntity;

        public bool EnableCheats => _cheatSettings.EnableCheats;
        public GameRootLoopEntity Cheats => _cheatEntity;
        public bool EnableRender => !EnableCheats || _cheatSettings.EnableRender;

        public UltimateCheatStarter(UltimateCheatSettings cheatSettings,
                                    GameRootLoopContext rootLoopContext)
        {
            _cheatSettings   = cheatSettings;
            _rootLoopContext = rootLoopContext;
        }

        public void Initialize()
        {
            if(!_cheatSettings.EnableCheats) return;
            _rootLoopContext.isCheats = true;
            _cheatEntity = _rootLoopContext.cheatsEntity;
        }
    }
}