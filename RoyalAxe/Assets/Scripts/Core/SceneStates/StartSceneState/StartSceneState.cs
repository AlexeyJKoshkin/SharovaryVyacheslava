using FluentBehaviourTree;

namespace Core.Launcher
{
    public class StartSceneState : RoyalAxeSceneState<IStateInfrastructure>
    {
        protected override void OnExecute(TimeData dt)
        {
            LoadScene(new MockSceneLoader(GameSceneType.Meta)); // Заканчиваем стейт загрузкой сцены меты. пока грузим сцену простой заглушкой
        }

        protected override void OnEnterState()
        {
            //в этот момент сцена загружена. Присутсвует дефолтный UI на сцене.
            // в этотм момент начинать всякие предазгрузочные дела (обновление версии, догрузка ресурсов, проверка сохранение, миграции, подгрузка конфигов)
            
            base.OnEnterState();
        }

        public StartSceneState(IStateInfrastructure stateInfrastructure) : base(stateInfrastructure) { }
    }
}