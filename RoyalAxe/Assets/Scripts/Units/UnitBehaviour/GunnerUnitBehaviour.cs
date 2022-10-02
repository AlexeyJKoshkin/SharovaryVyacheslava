using FluentBehaviourTree;
using UnityEngine;

namespace RoyalAxe.Units.UnitBehaviour
{
    public class GunnerUnitBehaviour : AbstractUnitBehaviour
    {
        [SerializeField] private bool _tempIsOnScreen;

        [Range(0, 2)] private float _maxDelayBeforeFirstAttack = 0.5f;


        private IBehaviourTreeNode _executedNode;
        private TimerNode _delayBeforeFirstAttackTimer;

        private RAAnimationEntity AnimationEntity => Unit.unitAnimationEntity.AnimationEntity;

        protected override void OnInit()
        {
            base.OnInit();
            _delayBeforeFirstAttackTimer = new TimerNode(Random.Range(0, _maxDelayBeforeFirstAttack));
            _executedNode = new BehaviourTreeBuilder()
                            .Selector<SelectorNode>("Выбор поведения моба")
                            .Sequence("Пока моб за экраном")
                            .Condition("Проверяем что моб за экраном", CheckCanLunchDelayTimer) // как только моб появится на экране
                            .Do(_delayBeforeFirstAttackTimer)                                   // ждем первый кулдаун
                            .End()
                            .Sequence("Цикл атаки мобом")
                            .Condition("Проверяем может ли моб струлять", CheckMobCanAttack) // запускаем цикл атаки моба
                            .Do("Моб Атакует", MobAttack)
                            .End()
                            .End()
                            .Build();
        }


        private bool CheckMobCanAttack(TimeData arg)
        {
            return
                _delayBeforeFirstAttackTimer.IsDone &&                   // выждали  задержку
                !AnimationEntity.isAttackTrigger &&                      // никого не атакуем в текущий кадр
                Unit.unitActiveSkill.SkillEntity.useCounterSkill.CanUse; // есть заряда на использование скила
        }

        private bool CheckCanLunchDelayTimer(TimeData arg)
        {
            return !_delayBeforeFirstAttackTimer.IsDone && _tempIsOnScreen;
        }


        private BehaviourTreeStatus MobAttack(TimeData arg)
        {
            //  HLogger.MobLog($"Mob {Unit.mob.Id} Attack");
            AnimationEntity.isAttackTrigger = true;
            AnimationEntity.ReplaceIsSit(false);
            return BehaviourTreeStatus.Success;
        }

        public override BehaviourTreeStatus Execute(TimeData time)
        {
            return _executedNode.Execute(time);
        }

        private void Fire()
        {
            Unit.unitActiveSkill.SkillEntity.isSkillUse = true;
            AnimationEntity.isAttackTrigger             = false;
            //проверить что моб может сесть
            bool isCanSit = true;
            AnimationEntity.ReplaceIsSit(isCanSit);
        }

        private void OnBecameVisible()
        {
            _tempIsOnScreen = true;
        }
    }
}