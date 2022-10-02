using System;
using FluentBehaviourTree;

namespace RoyalAxe.CharacterStat
{
    public class ElementalDamageNode : ParallelNode
    {
        private readonly PeriodicDamageInfluenceData _damage;
        private readonly TimerNode _bufDurationTimer;
        private readonly TimerNode _damageCooldownTimer;
        public Action DoDamageEvent;
        public Action EndBufEvent;

        public ElementalDamageNode(PeriodicDamageInfluenceData damage, Action doDamage = null, Action endBuff = null) :
            base("Елементальный урон по времени", 0, 5)
        {
            _damage              = damage;
            _damageCooldownTimer = new TimerNode(damage.DamageCooldown, "damage timer");
            _bufDurationTimer    = new TimerNode(damage.MagicDuration, "buff duration");
            DoDamageEvent        = doDamage;
            EndBufEvent          = endBuff;

            new BehaviourTreeBuilder().Push(this)
                                        .Sequence("Таймер нанесения урона")
                                            .Do(_damageCooldownTimer)
                                            .Do("Наносим урон", DoDamageCooldown)
                                        .End()
                                        .Sequence("Таймер бафа")
                                            .Do(_bufDurationTimer)
                                            .Do("Снимаем баф", DoBufTimer)
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
            _damageCooldownTimer.Timer = _damage.DamageCooldown;
            DoDamageEvent?.Invoke();
            return BehaviourTreeStatus.Success;
        }

        public void IncreaseDuration(float damageMagicDuration)
        {
            _bufDurationTimer.Timer += damageMagicDuration;
        }
    }
}