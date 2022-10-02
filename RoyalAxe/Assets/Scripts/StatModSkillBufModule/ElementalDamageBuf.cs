using System;
using System.Collections.Generic;
using System.Linq;
using FluentBehaviourTree;

namespace RoyalAxe.CharacterStat
{
    public class ElementalDamageBuf : BaseEntityBuf
    {
        //ссылка на того, кто навесил этот отрицательный баф.
        //нельзя хранить просто ссылку на сущность. т.к юнити может умереть пока действует елементальный урон
        public readonly Guid OwnerGuid;
        private readonly IUnitsInfluenceCalculator _influenceCalculator;
        public override string NodeName => "Maгический урон";

        private readonly DamageInfluenceData _damage;

        private readonly ElementalDamageNode _node;

        public ElementalDamageBuf(IUnitsInfluenceCalculator influenceCalculator, UnitsEntity owner, PeriodicDamageInfluenceData damage)
        {
            _influenceCalculator = influenceCalculator;
            OwnerGuid            = owner.uniqueUnitGUID.Guid;
            _damage              = damage;

            _node = new ElementalDamageNode(damage, DoDamageCooldown, DoBufTimer);
        }

        private void DoBufTimer()
        {
            RemoveFrom(Target);
        }

        private void DoDamageCooldown()
        {
            _influenceCalculator.GetBy(_damage.ElementalDamageType).ApplyTo(Target, _damage.Damage);
        }

        public override BehaviourTreeStatus Execute(TimeData time)
        {
            return _node.Execute(time);
        }

        //Создаем модификаторы, которые потом снимуться
        // в магическом уроне таких нет
        public override IEnumerable<ICharacterStatModificator> ApplyTempStats()
        {
            yield break;
        }

        public class ElementalBufApplyHelper : IDamageApplier
        {
            private readonly PeriodicDamageInfluenceData _damage;

            public ElementalBufApplyHelper(PeriodicDamageInfluenceData damage)
            {
                _damage              = damage;
            }

            public void Apply(UnitsEntity attacker, UnitsEntity target, IUnitsInfluenceCalculator unitsInfluenceCalculator)
            {
                var existsBuf =
                    target?.activeUnitBuff.FirstOrDefault(o => o is ElementalDamageBuf elementalDamageBuf
                                                            && elementalDamageBuf.OwnerGuid == attacker.uniqueUnitGUID.Guid
                                                            && elementalDamageBuf._damage.ElementalDamageType ==
                                                               _damage.ElementalDamageType) as ElementalDamageBuf;

                if (existsBuf == null)
                {
                    existsBuf = new ElementalDamageBuf(unitsInfluenceCalculator, attacker, _damage);
                    existsBuf.ApplyTo(target);
                }
                else
                {
                    existsBuf._node.IncreaseDuration(_damage.MagicDuration);
                }
            }
        }
    }
}