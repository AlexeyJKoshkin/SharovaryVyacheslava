using System.Collections.Generic;
using System.Linq;
using GameKit;

namespace Core.Launcher
{
    public interface ISceneLoaderProvider
    {
        T GetLoader<T>() where T : ISceneLoaderHelper;
    }

    public class SceneLoaderProvider : ISceneLoaderProvider
    {
        private readonly IReadOnlyList<ISceneLoaderHelper> _allHelpers;
        readonly Dictionary<GameSceneType,ISceneLoaderHelper> _dictionary = new Dictionary<GameSceneType, ISceneLoaderHelper>();

        public SceneLoaderProvider(IReadOnlyList<ISceneLoaderHelper> allHelpers)
        {
            _allHelpers = allHelpers;
            allHelpers.ForEach(e=> _dictionary.Add(e.TargetScene, e));
        }

        public ISceneLoaderHelper GetLoader(GameSceneType sceneType)
        {
            if (!_dictionary.TryGetValue(sceneType, out var result))
                result = new MockSceneLoader(sceneType);

            return result;
        }

        public T GetLoader<T>() where T : ISceneLoaderHelper
        {
            return (T) _allHelpers.FirstOrDefault(o => o is T);
        }
    }
}
