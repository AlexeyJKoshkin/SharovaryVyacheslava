using System.Collections.Generic;
using Entitas;

namespace RoyalAxe.EntitasSystems
{
    public class UnitsMatcherLibrary : BaseMatcherBuilder<UnitsEntity>
    {
        public UnitsMatcherLibrary()
        {
            _noneOfMathchers = new List<IMatcher<UnitsEntity>>() {UnitsMatcher.DestroyUnit};
        }

        public static IMatcherBuilder UnitInteractionMatcher(params IMatcher<UnitsEntity>[] triggerDefineMather)
        {
            triggerDefineMather ??= new IMatcher<UnitsEntity>[0];
            return new UnitsMatcherLibrary()
            {
                _defineMathchers = new List<IMatcher<UnitsEntity>>(triggerDefineMather)
                {
                    //добавляем три матча гарантирующие физическое взаимодейтсвие
                    UnitsMatcher.UnitsView,             // юнит на поле
                    UnitsMatcher.UnitPhysicCollider,    // коллайдер
                    UnitsMatcher.EnterPhysicInteraction //умеет взаимодействовать
                },
               
            };
        }

        public static IMatcherBuilder MovingUnits(params IMatcher<UnitsEntity>[] triggerDefineMather)
        {
            triggerDefineMather ??= new IMatcher<UnitsEntity>[0];
            return new UnitsMatcherLibrary()
            {
                _defineMathchers = new List<IMatcher<UnitsEntity>>(triggerDefineMather)
                {
                    UnitsMatcher.MoveSpeed,     // есть скорость
                    UnitsMatcher.MovingToPoint, // есть куда двигаться
                    UnitsMatcher.UnitsView      // и вьюшкой
                },
            };
        }

       
    }
}