﻿using System.Linq;
using Core;
using Entitas;
using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public class DestroyItemsAfterInteraction : PhysicalInteractionHandlerCompositeSystem
    {
        private readonly IUnitColliderDataBase _unitColliderData;

        public DestroyItemsAfterInteraction(UnitsContext unitsContext, IUnitColliderDataBase unitColliderData) : base(unitsContext)
        {
            _unitColliderData = unitColliderData;

            Add(UnitsMatcherLibrary.UnitInteractionMatcher(UnitsMatcher.Boson, UnitsMatcher.Mob),
                DestroyMobBosonAfterInteraction); // Уничтожение мобьих бозонов
            
            /*Add(UnitsMatcherLibrary.UnitInteractionMatcher(UnitsMatcher.PlayerBoson),
                DestroyPlayerBoson); // Уничтожение бозонов игрока*/

        }


        private void DestroyPlayerBoson(UnitsEntity playerBoson)
        {
            if(playerBoson.enterPhysicInteraction.Count == 0) return;
         
            //если бозон игрока провзаимодействовал с мобьим бозон - он НЕ уничтожается
           // var interactWithOtherBoson = playerBoson.enterPhysicInteraction.Select(o => _unitColliderData.Get(o)).Any(o => o.isMob && !o.isBoson);
           //уничтожаем только дополнительные бозоны
            playerBoson.isDestroyUnit = playerBoson.isAdditionalBoson;
            //   HLogger.TempLog($"{playerBoson.creationIndex} {interactWithOtherBoson} {playerBoson.isAdditionalBoson} { playerBoson.isDestroyUnit}");
        }
       

        private void DestroyMobBosonAfterInteraction(UnitsEntity mobBoson)
        {
            if(mobBoson.enterPhysicInteraction.Count == 0) return;
            //если мобий бозон провзаимодействовал с чем-то, что не явлется мобов - он уничтожается
            var interactWithSmth = mobBoson.enterPhysicInteraction.Select(o => _unitColliderData.Get(o)).Any(o => !o.isMob);
            mobBoson.isDestroyUnit = interactWithSmth;
        }

        
    }
}
