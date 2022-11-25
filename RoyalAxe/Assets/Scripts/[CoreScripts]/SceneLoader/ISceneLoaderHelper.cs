using System.Threading.Tasks;

namespace Core.Launcher
{
    public interface ISceneLoaderHelper
    {
        GameSceneType TargetScene { get; }
        Task UnloadResources();
        Task PreloadResources();
        
    }
}
