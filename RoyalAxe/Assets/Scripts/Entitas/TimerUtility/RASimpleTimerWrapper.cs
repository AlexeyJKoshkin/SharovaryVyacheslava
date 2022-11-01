using RoyalAxe.GameEntitas.Timer;

namespace RoyalAxe.EntitasSystems.TimerUtility
{
    public class RASimpleTimerWrapper : IRATimer
    {
        private readonly GameRootLoopEntity _timerEntity;

        public bool IsPause {
            get => _timerEntity.isPause;
            set => _timerEntity.isPause = value;
        }
        public bool IsRunning
        {
            get => _timerEntity.isEnabled && _timerEntity.isActiveTimer;
            set => _timerEntity.isActiveTimer = value;
        }

        public bool IsRepeat
        {
            get => _timerEntity.isRepeat;
            set => _timerEntity.isRepeat = value;
        }
        public ITimerInfo Info => _timerEntity.hasTimer ? default : _timerEntity.timer;

        public RASimpleTimerWrapper(GameRootLoopEntity entity)
        {
            _timerEntity = entity;
        }


        public void AddTickHandler(ITimerListener listener)
        {
            _timerEntity.AddTimerListener(listener);
        }

        public void RemoveTickHandler(ITimerListener listener)
        {
            _timerEntity.RemoveTimerListener(listener);
        }

        public void AddDoneHandler(IDoneTimerListener listener)
        {
            _timerEntity.AddDoneTimerListener(listener);
        }

        public void RemoveDoneHandler(IDoneTimerListener listener)
        {
            _timerEntity.RemoveDoneTimerListener(listener);
        }

        public void Run(float time)
        {
            if (time <= 0) return;

            _timerEntity.isPause = false;
            _timerEntity.ReplaceTimer(0, time);
            _timerEntity.isActiveTimer = true;
        }


        public void Destroy()
        {
            if (!_timerEntity.isEnabled) return;
            
            _timerEntity.isPause  = false;
            _timerEntity.isRepeat = false;
            if (_timerEntity.hasDoneTimerListener)
            {
                _timerEntity.RemoveDoneTimerListener();
            }
            _timerEntity.isDoneTimer = true; // таймер удалится в нужное время
        }
    }
}