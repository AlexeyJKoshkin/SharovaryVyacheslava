namespace Core.Launcher
{
    public interface IMainGameContext
    {
        void HandleNewState(IProjectSceneState newSceneState);
    }
}