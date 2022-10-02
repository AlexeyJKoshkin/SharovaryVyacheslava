using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public class EnemyAnimatorData
    {
        public const string RunningLayerName = "Running";
        public const string DefaultLayerName = "Base Layer";
        public const string DieTriggerName = "Die";
        public const string HitTriggerName = "Hit";
        public const string IdleAnimationName = "Idle";
        public const string AttackTriggerName = "Attack";
        public const string IsSitBoolName = "IsSit";

        public int RunningLayer { get; private set; } = 1;
        public int DefaultLayer { get; private set; } = 0;
        public int DieTrigger { get; private set; }
        public int AttackTrigger { get; private set; }
        public int HitTrigger { get; private set; }
        public int IdleAnimation { get; private set; }
        public int IsSit { get; private set; }

        public EnemyAnimatorData()
        {
            Update();
        }

        public void Update()
        {
            DieTrigger    = Animator.StringToHash(DieTriggerName);
            HitTrigger    = Animator.StringToHash(HitTriggerName);
            AttackTrigger = Animator.StringToHash(AttackTriggerName);
            IdleAnimation = Animator.StringToHash(IdleAnimationName);
            IsSit         = Animator.StringToHash(IsSitBoolName);
        }
    }
}