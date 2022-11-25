namespace Core.Launcher
{
    public interface IProjectEntitasRuntimeSystemBuilder
    {
        Feature BuildUpdate(string featureName);

        Feature BuildPauseableUpdate(string featureName);
    }

    public interface IRoyalAxeCoreSystemsBuilder : IProjectEntitasRuntimeSystemBuilder { }
}