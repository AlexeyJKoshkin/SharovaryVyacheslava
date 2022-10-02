using RoyalAxe.GameEntitas.Timer;

namespace RoyalAxe.EntitasSystems.TimerUtility
{
    public interface IRATimer
    {
        bool IsPause { get; }
        bool IsRunning { get; set; }
        bool IsRepeat { get; }
        ITimerInfo Info { get; }
        void AddTickHandler(ITimerListener listener);
        void RemoveTickHandler(ITimerListener listener);
        void AddDoneHandler(IDoneTimerListener listener);
        void RemoveDoneHandler(IDoneTimerListener listener);
        void Destroy();
        void Run(float time);
    }
}