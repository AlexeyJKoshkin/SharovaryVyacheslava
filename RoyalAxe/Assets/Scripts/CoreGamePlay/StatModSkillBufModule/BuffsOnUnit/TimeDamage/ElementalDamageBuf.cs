using System;
using System.Collections.Generic;
using System.Linq;
using FluentBehaviourTree;
using RoyalAxe.EntitasSystems;
using RoyalAxe.GameEntitas;

namespace RoyalAxe.CharacterStat
{
    public class ElementalDamageBuf : BaseEntityBuf
    {
        //ссылка на того, кто навесил этот отрицательный баф.
        //нельзя хранить просто ссылку на сущность. т.к юнити может умереть пока действует елементальный урон
        public readonly Guid OwnerGuid;
        private readonly IUnitsInfluenceCalculator _influenceCalculator;
        public override string NodeName => "Maгический урон";

        private readonly SkillConfigDef.Damage _damage;

        private readonly ElementalDamageNode _node;

        public ElementalDamageBuf(IUnitsInfluenceCalculator influenceCalculator, UnitsEntity owner, SkillConfigDef.Damage damage)
        {
            _influenceCalculator = influenceCalculator;
            OwnerGuid            = owner.uniqueUnitGUID.Guid;
            _damage              = damage;

            _node = new ElementalDamageNode(damage, IsTargetAlive, DoDamageCooldown, DoBufTimer);
        }

        private bool IsTargetAlive(TimeData arg) // если цель жива
        {
            return !Target.isDeadUnit && Target.health.CurrentValue > 0;
        }

        private void DoBufTimer()
        {
            Target.RemoveBuf(this);
        }

        //применить урон к цели
        private void DoDamageCooldown()
        {
            _influenceCalculator.ApplyElementalTimingDamage(Target, _damage.SingleDamageInfo);
            
             
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

        public class ElementalBufApplyHelper : IPeriodicInfluenceApplier
        {
            public SkillConfigDef.Damage DamageData => _damage;

            private readonly SkillConfigDef.Damage _damage;
            private readonly IUnitDamageApplierFactory _unitDamageApplierFactory;

            public ElementalBufApplyHelper(SkillConfigDef.Damage damage, IUnitDamageApplierFactory unitDamageApplierFactory)
            {
                _damage                   = damage;
                _unitDamageApplierFactory = unitDamageApplierFactory;
            }

            public void Apply(UnitsEntity attacker, UnitsEntity target)
            {
                if (target == null || !target.isEnabled) return;
                // в списке активных бафов ищем                      элементальный урон
                var existsBuf = target.activeUnitBuff.FirstOrDefault(o => o is ElementalDamageBuf elementalDamageBuf
                                                                          //который навесил текущий юнит
                                                                       && elementalDamageBuf.OwnerGuid == attacker.uniqueUnitGUID.Guid
                                                                          // такой же
                                                                       && elementalDamageBuf._damage.ElementalDamageType ==
                                                                          _damage.ElementalDamageType) as ElementalDamageBuf;

                if (existsBuf != null)
                {
                    existsBuf._node.IncreaseDuration(_damage.MagicDuration);
                }
                else
                {
                    var buff = _unitDamageApplierFactory.CreateElementalDamageBuf(attacker, _damage);

                    target.ApplyBuf(buff);
                }
            }
        }
    }
}