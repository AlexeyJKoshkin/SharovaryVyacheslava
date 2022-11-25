using System.Threading.Tasks;

namespace Core.Launcher
{
    public abstract class AbstractSceneLoader : ISceneLoaderHelper
    {
        public abstract GameSceneType TargetScene { get; }
        public abstract Task UnloadResources();
        public abstract Task PreloadResources();
    }
}
