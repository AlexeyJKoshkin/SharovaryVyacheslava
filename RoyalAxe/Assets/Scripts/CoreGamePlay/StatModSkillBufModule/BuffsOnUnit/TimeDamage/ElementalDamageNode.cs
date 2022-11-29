using System;
using FluentBehaviourTree;

namespace RoyalAxe.CharacterStat
{
    public class ElementalDamageNode : SequenceNode
    {
        private readonly TimerNode _bufDurationTimer;
        private readonly TimerNode _damageCooldownTimer;
        private readonly ActionNode _removeBuf;
        public Action DoDamageEvent;
        public Action EndBufEvent;

        public ElementalDamageNode(PeriodicDamageInfluenceData damage,
                                   Func<TimeData, bool> checkIsTargetIdDead,
                                   Action doDamage = null,
                                   Action endBuff = null) :
            base("Наносим урон размазанный по времени")
        {
            _damageCooldownTimer = new TimerNode(damage.DamageCooldown, "damage timer");
            _bufDurationTimer    = new TimerNode(damage.MagicDuration, "buff duration");
            DoDamageEvent        = doDamage;
            EndBufEvent          = endBuff;
            
            _removeBuf = new ActionNode("Снимаем бафф", DoBufTimer);

            new BehaviourTreeBuilder().Parent(this)
                                      .Parallel("Счетчики", 0,5)
                                        .Sequence("Если цель мерта, снимаем баф")
                                            .Condition("Цель мертва", checkIsTargetIdDead)
                                            .Do(_removeBuf)
                                        .Sequence("Таймер нанесения урона")
                                            .Do(_damageCooldownTimer)
                                            .Do("Наносим урон", DoDamageCooldown)
                                        .End()
                                        .Sequence("Таймер бафа")
                                            .Do(_bufDurationTimer)
                                            .Do(_removeBuf)
                                        .End()
                                      .End()
                                 .Build();
        }

        private BehaviourTreeStatus DoBufTimer(TimeData arg)
        {
            EndBufEvent?.Invoke();
            return BehaviourTreeStatus.Success;
        }

        private BehaviourTreeStatus DoDamageCooldown(TimeData arg)
        {
            _damageCooldownTimer.ResetTimer();
            DoDamageEvent?.Invoke();
            return BehaviourTreeStatus.Success;
        }

        public void IncreaseDuration(float damageMagicDuration)
        {
            _bufDurationTimer.Timer += damageMagicDuration;
        }
    }
}