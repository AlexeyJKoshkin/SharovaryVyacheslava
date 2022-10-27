using System.Collections.Generic;
using FluentBehaviourTree;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CharacterStat
{
    public interface IModificatorProvider
    {
        IEnumerable<ICharacterStatModificator> ApplyTempStats();
        IEnumerable<ICharacterStatModificator> ApplyPermanentStatMods();
    }

    /// <summary>
    ///     Абстрактный баф наложенный на юнита
    /// </summary>
    public abstract class BaseEntityBuf : IEntityBuff, IModificatorProvider
    {
        public UnitsEntity Target => _helper.Target;
        public abstract string NodeName { get; }
        private readonly EquipableSetterHelper _helper;

        public BaseEntityBuf()
        {
            _helper = new EquipableSetterHelper(this);
        }

        public virtual void ApplyTo(UnitsEntity target)
        {
            _helper.ApplyTo(target);
        }

        public virtual IEnumerable<ICharacterStatModificator> ApplyTempStats()
        {
            yield break;
        }

        public void RemoveFrom(UnitsEntity owner)
        {
            _helper.RemoveFrom(owner);
            Target.RemoveBuf(this);
        }

        //Применять перманентные изменения
        public virtual IEnumerable<ICharacterStatModificator> ApplyPermanentStatMods()
        {
            yield break;
        }


        public abstract BehaviourTreeStatus Execute(TimeData time);
    }
}