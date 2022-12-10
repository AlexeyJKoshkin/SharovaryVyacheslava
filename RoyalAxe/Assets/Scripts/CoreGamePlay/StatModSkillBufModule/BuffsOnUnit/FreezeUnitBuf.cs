using System;
using System.Collections.Generic;
using FluentBehaviourTree;
using RoyalAxe.CoreGamePlay;

namespace RoyalAxe.Units.Stats
{
    public  class FreesBuffSpeedModificator : IModificatorProvider
    {
        private readonly float _speedDecreaseValue;
        public FreesBuffSpeedModificator(float speedDecreaseValue)
        {
            _speedDecreaseValue = speedDecreaseValue;
        }

        public IEnumerable<ICharacterStatModificator> ApplyTempStats(UnitsEntity target)
        {
            yield break;
        }

        public void ApplyPermanentStatMods(UnitsEntity target)
        {
            target.moveSpeed.ChangeValue().FromActualCurrent(-_speedDecreaseValue).ApplyPermanentMod();
        }
    }
}