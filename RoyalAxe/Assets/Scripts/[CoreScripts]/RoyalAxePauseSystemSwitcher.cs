namespace Core.Installers 
{
    public class RoyalAxePauseSystemSwitcher : IRoyalAxePauseSystemSwitcher
    {
        private readonly IRoyalAxePauseSystemSwitcher _receiver;
        private readonly GameRootLoopContext _context;
        public RoyalAxePauseSystemSwitcher(GameRootUnityCallbackReceiver receiver, GameRootLoopContext context)
        {
            _receiver = receiver;
            _context  = context;
            _context.CreateEntity().AddGamePause(false);
        }

        public void SetPause()
        {
            _context.gamePauseEntity.ReplaceGamePause(true);
            _receiver.SetPause();
        }

        public void UnPause()
        {
            _context.gamePauseEntity.ReplaceGamePause(false);
            _receiver.UnPause();
        }

        public void SetState(bool isPause)
        {
            if (_context.gamePause.IsPause == isPause) return;

            if(isPause)
                UnPause();
            else
                SetPause();
        }
    }
}