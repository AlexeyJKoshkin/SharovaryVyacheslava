using System.Collections;
using FluentBehaviourTree;
using UnityEngine.SceneManagement;

namespace Core.Launcher
{
    public class SceneLoaderState : AbstractBTNode, ISceneLoaderSceneState
    {
        private readonly ISceneLoaderUnityView _view;

        public SceneLoaderState(ISceneLoaderUnityView view)
        {
            _view = view;
        }

        public GameSceneType CurrentScene { get; private set; }

        private GameSceneType _currentLoadingScene;
        private BehaviourTreeStatus _result;

        public void LoadScene(GameSceneType gameSceneType)
        {
            _currentLoadingScene = gameSceneType;
            //подготовка к загрузке сцены
        }

        private IEnumerator MockLoadScene(GameSceneType gameSceneType)
        {
            var operation = SceneManager.LoadSceneAsync((int) gameSceneType);
            while (operation.isDone) yield return null;
            HLogger.LogInfo($"SceneLoaded {gameSceneType}");
        }

        void IFMSState.ExitState()
        {
            _result = BehaviourTreeStatus.Failure; // вызов исполнения ноды в неактивном режиме вызовет срабатывание фейл
        }

        void IFMSState.EnterState()
        {
            //   Debug.LogError("ENter Load");
            //всю логику (подготовка, UI, старт загрузки, показ доп модулей и всякое такое выполнять в этом методе)
            //Желательно сделать одну большую макаронину и разбить на таски или отдельные задачи, которые выполняются последовательно.
            _view.StartCoroutine(MockLoadScene(_currentLoadingScene)); // пока просто грузим сцену без ничего.
            //добавить показ UI/
            //добавит прогресс бар загрузки если надо
            //выбор окон
            CurrentScene = _currentLoadingScene;
            _result      = BehaviourTreeStatus.Running;
        }


        public override BehaviourTreeStatus Execute(TimeData time)
        {
            return _result;
        }

        public override string NodeName => "Загрузчик сцены";
    }
}