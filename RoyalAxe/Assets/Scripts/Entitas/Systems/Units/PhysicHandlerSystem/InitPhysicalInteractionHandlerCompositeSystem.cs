using System.Linq;
using Core;
using RoyalAxe.GameEntitas;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public class InitPhysicalInteractionHandlerCompositeSystem : PhysicalInteractionHandlerCompositeSystem
    {
     
        private readonly IUnitColliderDataBase _unitColliderData;
        public InitPhysicalInteractionHandlerCompositeSystem(IUnitColliderDataBase unitColliderData, UnitsContext unitsContext) : base(unitsContext)
        {
            _unitColliderData = unitColliderData;
            Add(UnitsMatcherLibrary.UnitInteractionMatcher(UnitsMatcher.Mob), TrySetPlayerAsTargetToMob);
          
            Add(UnitsMatcherLibrary.UnitInteractionMatcher(UnitsMatcher.PlayerBoson), HandlePlayerSkill); //
        }
        
        //пока обрабатываем только взаимодействие с игроком.
        private void TrySetPlayerAsTargetToMob(UnitsEntity mob)
        {
            if(mob.enterPhysicInteraction.Count == 0) return;
            // моб при взаимодействии с игроком - навешивает урон
            if ( mob.enterPhysicInteraction.Contains(Player.unitPhysicCollider.PhysicCollider))
            {
                mob.ReplacePossibleTargets(mob.possibleTargets.Add(Player)); //добавляем игрока в список целей 
            }
        }


        //попали скилом игрока по мобу
        private void HandlePlayerSkill(UnitsEntity skill)
        {
            var mobInteraction = skill.enterPhysicInteraction.Collection; // все с чем хочет провзаимодействовать скилл
            var mobs = mobInteraction.Select(o => _unitColliderData.Get(o)).Where(o => !o.isPlayer && !o.isBoson).ToArray();
            if (mobs.Length > 0)
            {
                //Собираем всех мобов
                var colldection =  skill.possibleTargets.Add(mobs[0]); // добавляем только одного моба
                skill.ReplacePossibleTargets(colldection); 
                skill.ReplaceMovingToPoint(new RichPointAdapter());             // закончили движение
            }
        }
    }
}
