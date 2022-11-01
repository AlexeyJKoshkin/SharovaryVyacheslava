using Core;
using Entitas;
using FluentBehaviourTree;

namespace RoyalAxe.CoreLevel {
    public class ShowWinWindowNode : SequenceNode
    {
        private readonly IRoyalAxeCoreMap _map;
        private readonly IWinLevelUICommand _winLevelUiCommand;
        private readonly ILevelWaveProvider _levelWaveProvider;

        private bool _isWin;
        public ShowWinWindowNode(IRoyalAxeCoreMap map,
                                 CoreGamePlayContext context,
                                 IWinLevelUICommand winLevelUiCommand,
                                 ILevelWaveProvider levelWaveProvider) : base("Проверяем что победили")
        {
            _map               = map;
            _winLevelUiCommand = winLevelUiCommand;
            _levelWaveProvider = levelWaveProvider;

            /*_levelReadyGroup = context.GetGroup(Matcher<CoreGamePlayEntity>.AllOf(CoreGamePlayMatcher.WaveFinished)       // волна закончилась
                                                                           .NoneOf(CoreGamePlayMatcher.WaveMobReady,      //мобов нету
                                                                                   CoreGamePlayMatcher.WizardShopReady)); // нет магазина*/

            new BehaviourTreeBuilder().Sequence(this)
                                      .Condition("Волны закончились, мобов больше нет", CheckUserWin)
                                      .Do("Показать экран победы", WinGameAction).End().Build();
        }

        private float _timer;

        private bool CheckUserWin(TimeData arg)
        {
            if (_isWin) return false;
            /*_timer += arg.deltaTime;
            return _timer > 3;*/
            //волны закончились, можно грузить следующую волну, 
            return
                !_levelWaveProvider.HasWave &&
                 _map.CurrentMobAmount == 0; //а мобы не появляются
        }

        private BehaviourTreeStatus WinGameAction(TimeData arg)
        {
            _isWin = true;
            _winLevelUiCommand.ExecuteCommand();
            return BehaviourTreeStatus.Success;
        }
    }
}