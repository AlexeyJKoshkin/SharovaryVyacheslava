using FluentBehaviourTree;
using Lean.Touch;
using RoyalAxe.GameEntitas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RoyalAxe.Units.UnitBehaviour
{
    public class PlayerUnitBehaviour : AbstractUnitBehaviour
    {
        private RAAnimationEntity AnimationEntity => Unit.unitAnimationEntity.AnimationEntity;
        private IBehaviourTreeNode _executedNode;

        private bool _wasTouch;
        private Vector3 _touchPosition;

        protected override void OnInit()
        {
            base.OnInit();
            LeanTouch.OnFingerDown += OnUserTouchScreen;

            _executedNode = new BehaviourTreeBuilder()
                            .Sequence("Цикл атаки")
                            .Condition("Проверяем может стрельнул ли игрок", CheckUserAttack)
                            .Do("Аткауем", PlayerAttack)
                            .End()
                            .Build();
        }

        public override BehaviourTreeStatus Execute(TimeData time)
        {
            return _executedNode.Execute(time);
        }


        private BehaviourTreeStatus PlayerAttack(TimeData arg)
        {
            // _wasTouch = false;
            Unit.unitActiveSkill.SkillEntity.ReplaceMovingToPoint(new SimpleVector2Adapter(_touchPosition)); // задаем текущему скилу endPoint
            AnimationEntity.isAttackTrigger             = true;
            Unit.unitActiveSkill.SkillEntity.isSkillUse = true;
            AnimationEntity.isAttackTrigger             = false;
            _wasTouch                                   = false;
            return BehaviourTreeStatus.Success;
        }

        private bool CheckUserAttack(TimeData arg)
        {
            var canUse =
                !AnimationEntity.isAttackTrigger && // никого не атакуем в текущий кадр
                _wasTouch &&
                Unit.unitActiveSkill.SkillEntity.useCounterSkill.CanUse; // есть заряда на использование скила
            return canUse;
        }

        [Button]
        private void Fire()
        {
            Unit.unitActiveSkill.SkillEntity.isSkillUse = true;
            AnimationEntity.isAttackTrigger             = false;
        }

        private void OnDestroy()
        {
            LeanTouch.OnFingerDown -= OnUserTouchScreen;
        }

        private void OnUserTouchScreen(LeanFinger touch)
        {
            if (touch.IsOverGui || !Unit.unitActiveSkill.SkillEntity.useCounterSkill.CanUse)
            {
                return;
            }

            _wasTouch      = true;
            _touchPosition = touch.GetWorldPosition(100, Camera.current);
        }
    }
}