using System.Collections.Generic;
using FluentBehaviourTree;

namespace RoyalAxe.CharacterStat
{
    /// <summary>
    /// Заморозка
    /// </summary>
    public class FreezeUnitBuf : BaseEntityBuf
    {
        public override string NodeName { get; } = "Заморозка";

        private float _speedDecreaseValue;
        public FreezeUnitBuf(float speedDecreaseValue)
        {
            _speedDecreaseValue = speedDecreaseValue;
        }

        public override BehaviourTreeStatus Execute(TimeData time)
        {
            //баф висит все время пока его не снимут/ либо юнит не умрет
            return BehaviourTreeStatus.Success;
        }

        public override IEnumerable<ICharacterStatModificator> ApplyPermanentStatMods()
        {
            yield return this.Target.moveSpeed.ChangeValue().FromActualCurrent(-_speedDecreaseValue);
        }
    }
}
