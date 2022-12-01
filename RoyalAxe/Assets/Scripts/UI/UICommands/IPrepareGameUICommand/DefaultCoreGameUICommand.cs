namespace RoyalAxe.CoreLevel 
{
    public class DefaultCoreGameUIPrepareCommand : IPrepareGameUICommand
    {
        private readonly CoreGameSceneUIView _coreGameSceneUiView;
        private readonly WinWindowView _winWindowController;
        private readonly LoseWindowView _loseWindowController;
        private readonly BuffSelectWindowView _buffSelectWindowView;
        private readonly Contexts _contexts;
        public DefaultCoreGameUIPrepareCommand(CoreGameSceneUIView coreGameSceneUiView,
                                        Contexts contexts,
                                        LoseWindowView loseWindowController,
                                        WinWindowView winWindowController,
                                        BuffSelectWindowView buffSelectWindowView)
        {
            _coreGameSceneUiView  = coreGameSceneUiView;
            _contexts             = contexts;
            _loseWindowController = loseWindowController;
            _winWindowController  = winWindowController;
            _buffSelectWindowView = buffSelectWindowView;
        }

        public void PrepareUIStartGame()
        {
            _coreGameSceneUiView.InitEntity( _contexts.units.playerEntity);
            _coreGameSceneUiView.InitEntity( _contexts.coreGamePlay.playerEntity);
            _coreGameSceneUiView.InitEntity(_contexts.coreGamePlay.GetGroup(CoreGamePlayMatcher.LevelNumber).GetSingleEntity());
            
            
            _winWindowController.Close();
            _loseWindowController.Close();
            _buffSelectWindowView.Close();
        }
    }
}