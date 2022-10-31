namespace RoyalAxe.CoreLevel 
{
    public interface IMobAtLevelDirector : IDoneTimerListener
    {
        void StartWaveImmediate();
        void StopSpawn();
    }
}