using UnityEngine;

namespace Core.Launcher
{
    /// <summary>
    ///     Юнити объект загрузчика сценн.
    /// </summary>
    public class SceneLoaderUnityView : MonoBehaviour, ISceneLoaderUnityView
    {
        [SerializeField] private GameObject _ui; //cсылки на Ui Объекты. тут их может быть много в зависимости от нужд
    }
}