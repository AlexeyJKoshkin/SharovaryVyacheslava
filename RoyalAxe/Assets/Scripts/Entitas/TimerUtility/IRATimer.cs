using RoyalAxe.GameEntitas.Timer;

namespace RoyalAxe.EntitasSystems.TimerUtility
{
    public interface IRATimer
    {
        bool IsPause { get; set; }
        bool IsRunning { get; set; }
        bool IsRepeat { get; set; }
        ITimerInfo Info { get; }
        void AddTickHandler(ITimerListener listener);
        void RemoveTickHandler(ITimerListener listener);
        void AddDoneHandler(IDoneTimerListener listener);
        void RemoveDoneHandler(IDoneTimerListener listener);
        void Destroy();
        void Run(float time);
    }
}