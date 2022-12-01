using System.Collections.Generic;
using FluentBehaviourTree;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CharacterStat
{
    public interface IModificatorProvider
    {
        IEnumerable<ICharacterStatModificator> ApplyTempStats();
        void ApplyPermanentStatMods();
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

        public void RemoveFrom(UnitsEntity owner, bool isSilent = false)
        {
            _helper.RemoveFrom(owner);
            if(isSilent) return;
            Target.RemoveBuf(this);
        }

        //Применять перманентные изменения
        public virtual void ApplyPermanentStatMods()
        {
        }


        public abstract BehaviourTreeStatus Execute(TimeData time);
    }
}