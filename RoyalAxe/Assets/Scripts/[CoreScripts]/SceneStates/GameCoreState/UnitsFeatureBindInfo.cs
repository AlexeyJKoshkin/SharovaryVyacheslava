using RoyalAxe.EntitasSystems;
using RoyalAxe.GameEntitas;

namespace Core.Launcher
{
    class UnitsFeatureBindInfo : FeatureBindInfo
    {
        public UnitsFeatureBindInfo()
        {
            FeatureName = "Units";
            BindSystems();
        }

        private void BindSystems()
        {
            //инициализация
                 Bind<SetPlayerUnitToChunkRootSystem>()     // Установка игрока в родительский чанк
                .Bind<SetUnitsToChunkRootSystem>()     // Установка юнитов в родительский чанк
                .Bind<SetAdditionalBosonColorSystem>() // покраска дополнотельных бозонов
                .Bind<UpdateMoveSpeedNavMeshAgentsSystem>() // обновляем скорость у мобов с навмешем при измении стата скорость
                //игровой цикл

                //cкилы
                .Bind<DefaultGunnerSkillExecuteSystem>() // пробуем начать использовать скилл пушкаря
                .Bind<DefaultPlayerSkillExecuteSystem>() // пробуем жахнуть игроком
                //взаимодействие
                .Bind<UnitColliderDataBaseSystem>()                    // Словарь моб - коллайдер
                .Bind<InitPhysicalInteractionHandlerCompositeSystem>() // логика обработки физического взаимодейтсвия
                .Bind<UpdateUnitBehaviourSystem>()                     // обновляем поведение мобов
                .Bind<MeleeAttackCalculationSystem>()                          // обработка атаки милишников todo: возможно стоит перенсти в поведние
                .Bind<UnitBuffTickSystem>()                            // тик переодического урона
                .Bind<DestroyItemsAfterInteraction>()                  // отдаем команду на уничтожение юнитов на поле

                //движение 
                .Bind<UnitsMovingSystem>()                          // двигаем юнитов
                .Bind<SetPauseNavMeshSystem>()
                // Если юнит выходит за пределы экрана его объект и сущность уничтожается.
                // мобы всегда идут за экран, где они умирают. поэтому не обрабатываем финиш мобов.
                // часть бозонов может улететеь за экран.
                .Bind<CheckBosonRichPosition>()                     // обработка окончания движения бозонов (что-то во что-то попало)
                .Bind<RestorePlayerSkillUsageAfterBosonDestroyed>() // если топоры игрока долетели, то увеличиваем счетчик

                //блок анимаций
                .Bind<DefaultMobAnimationSystem>()
                .Bind<RangeMobAnimationSystem>();

        }
    }
}