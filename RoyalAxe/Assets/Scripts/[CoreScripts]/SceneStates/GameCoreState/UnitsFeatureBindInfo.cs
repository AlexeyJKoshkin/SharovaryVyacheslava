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
                //игровой цикл

                //cкилы
                .Bind<DefaultGunnerSkillExecuteSystem>() // пробуем начать использовать скилл пушкаря
                .Bind<DefaultPlayerSkillExecuteSystem>() // пробуем жахнуть игроком
                //взаимодействие
                .Bind<UnitColliderDataBaseSystem>()                    // Словарь моб - коллайдер
                .Bind<InitPhysicalInteractionHandlerCompositeSystem>() // обработка логика обработки физического взаимодейтсвия
                .Bind<UpdateUnitBehaviourSystem>()                     // обновляем поведение мобов
                .Bind<MeleeMobAttackSystem>()                          // обработка атаки милишников todo: возможно стоит перенсти в поведние
                .Bind<UnitBuffTickSystem>()                            // тик переодического урона
                .Bind<DestroyItemsAfterInteraction>()                  // отдаем команду на уничтожение юнитов на поле

                //движение
                .Bind<UnitsMovingSystem>()                          // двигаем юнитов
                .Bind<CheckBosonRichPosition>()                     // проверяем мб, кто-то дошел до конца
                .Bind<RestorePlayerSkillUsageAfterBosonDestroyed>() // если топоры игрока долетели, то увеличиваем счетчик

                //блок анимаций
                .Bind<DefaultMobAnimationSystem>()
                .Bind<RangeMobAnimationSystem>();

        }
    }
}