using Core.Data.Provider;

namespace Core.Launcher
{
    public interface ISceneStateSettings
    {
        /// <summary>
        ///     данные для сцены
        /// </summary>
      //  DataBox[] SceneData { get; }
        string NodeName { get; }
    }
}