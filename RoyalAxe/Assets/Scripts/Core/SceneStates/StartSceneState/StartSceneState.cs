using FluentBehaviourTree;
using UnityEngine;

namespace Core.Launcher
{
    public class StartSceneState : AbstractSceneStateScriptable
    {
        [SerializeField] public GameSceneType NextSceneLoad;

        protected override void OnExecute(TimeData dt)
        {
            //чекаем процессы запущенные ранее
            LoadScene(NextSceneLoad); // Заканчиваем стейт загрузкой сцены меты
        }

        protected override void OnEnterState()
        {
            //в этот момент сцена загружена. Присутсвует дефолтный UI на сцене.
            // в этотм момент начинать всякие предазгрузочные дела (обновление версии, догрузка ресурсов, проверка сохранение, миграции, подгрузка конфигов)
            base.OnEnterState();
        }
    }
}