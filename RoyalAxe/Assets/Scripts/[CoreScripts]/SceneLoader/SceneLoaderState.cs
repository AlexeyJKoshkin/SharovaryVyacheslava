using System.Collections;
using FluentBehaviourTree;
using UnityEngine.SceneManagement;

namespace Core.Launcher
{
    public class SceneLoaderState : AbstractBTNode,ISceneLoaderSceneState
    {
        private readonly ISceneLoaderUnityView _view;

        public SceneLoaderState(ISceneLoaderUnityView view)
        {
            _view = view;
        }

        public GameSceneType CurrentScene { get; private set; }

        private ISceneLoaderHelper _currentLoadingScene;
        private BehaviourTreeStatus _result;

        public void LoadScene(ISceneLoaderHelper sceneLoader)
        {
            _currentLoadingScene = sceneLoader;
            //подготовка к загрузке сцены
        }

        void IFMSState.ExitState()
        {
            _result = BehaviourTreeStatus.Failure; // вызов исполнения ноды в неактивном режиме вызовет срабатывание фейл
        }

        async void IFMSState.EnterState()
        {
            //   Debug.LogError("ENter Load");
            //всю логику (подготовка, UI, старт загрузки, показ доп модулей и всякое такое выполнять в этом методе)
            //Желательно сделать одну большую макаронину и разбить на таски или отдельные задачи, которые выполняются последовательно.

            //добавить показ UI/
            //добавит прогресс бар загрузки если надо

            if (_currentLoadingScene != null)
            {
                await _currentLoadingScene.UnloadResources();
            }

            _view.StartCoroutine(MockLoadScene(_currentLoadingScene)); // пока просто грузим сцену без ничего.
            //выбор окон
            _result = BehaviourTreeStatus.Running;
        }

        private IEnumerator MockLoadScene(ISceneLoaderHelper sceneLoaderHelper)
        {
            // По идее тут надо выгружать старые ресурсы

            var operation = SceneManager.LoadSceneAsync((int) sceneLoaderHelper.TargetScene);
            while (operation.isDone) yield return null;
            HLogger.LogInfo($"SceneLoaded {sceneLoaderHelper.TargetScene}");
            CurrentScene = _currentLoadingScene.TargetScene;
            PreloadResources(sceneLoaderHelper);
        }

        private async void PreloadResources(ISceneLoaderHelper sceneLoaderHelper)
        {
            await sceneLoaderHelper.PreloadResources();
            _result = BehaviourTreeStatus.Success;
        }

        public override BehaviourTreeStatus Execute(TimeData time)
        {
            return _result;
        }

        public override string NodeName => "Загрузчик сцены";
    }
}
