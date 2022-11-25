using System.Threading.Tasks;

namespace Core.Launcher
{
    public class MockSceneLoader : AbstractSceneLoader
    {
        public override GameSceneType TargetScene { get;  }

        public MockSceneLoader(GameSceneType sceneType)
        {
            TargetScene = sceneType;
        }

       
        public override Task UnloadResources()
        {
           return Task.CompletedTask;
        }

        public override Task PreloadResources()
        {
            return Task.CompletedTask;
        }
    }
}
