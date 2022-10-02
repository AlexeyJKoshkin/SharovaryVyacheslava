namespace Core.Installers
{
    public interface IRoyalAxePauseSystemSwitcher
    {
        void SetPause();
        void UnPause();

        void SetState(bool isPause);
    }
}