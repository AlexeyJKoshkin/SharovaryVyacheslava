namespace Core.Launcher
{
    /// <summary>
    ///     Хелпер для загрузки сцен.
    /// </summary>
    public interface ISceneLoader
    {
        GameSceneType CurrentScene { get; }
        void LoadScene(ISceneLoaderHelper sceneLoaderHelper);
    }


    public enum GameSceneType
    {
        StartScene = 0,
        Meta = 1,
        Core= 2,
    }
}