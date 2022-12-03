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

        public ElementalDamageNode(SkillConfigDef.Damage damage,
                                   Func<TimeData, bool> checkIsTargetIdDead,
                                   Action doDamage = null,
                                   Action endBuff = null) :
            base("Наносим урон размазанный по времени")
        {
            _damageCooldownTimer = new TimerNode(damage.DamageCooldown, "damage timer");
            _bufDurationTimer    = damage.MagicDuration <0 ? null : new TimerNode(damage.MagicDuration, "buff duration");
            DoDamageEvent        = doDamage;
            EndBufEvent          = endBuff;
            _removeBuf = new ActionNode("Снимаем бафф", DoBufTimer);

            BuildTree(checkIsTargetIdDead);

           
        }

        private void BuildTree(Func<TimeData, bool> checkIsTargetIdDead)
        {
            new BehaviourTreeBuilder().Parent(this)
                                        .Parallel("Счетчики", 0,5)
                                            .Sequence("Если цель мерта, снимаем баф")
                                                .Condition("Цель мертва", checkIsTargetIdDead)
                                                .Do(_removeBuf)
                                            .Sequence("Таймер нанесения урона")
                                                .Do(_damageCooldownTimer)
                                                .Do("Наносим урон", DoDamageCooldown)
                                            .End()
                                      .End()
                                      .Build();

            if (_bufDurationTimer != null) // удаляем баф, только если есть врем действия
            {
                var child = new BehaviourTreeBuilder().Sequence("Таймер бафа")
                                                      .Do(_bufDurationTimer)
                                                      .Do(_removeBuf)
                                                      .End().Build();
                this.AddChild(child);
            }
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