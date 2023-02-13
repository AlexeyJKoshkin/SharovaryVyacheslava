using UnityEngine;

namespace RoyalAxe.EntitasSystems
{
    public static class AnimationEntityActions
    {
        public static readonly EnemyAnimatorData AnimData = new EnemyAnimatorData();

        public static void PlayRun(RAAnimationEntity entity, Animator animator)
        {
            animator.SetLayerWeight(AnimData.RunningLayer, 1);
            animator.Play(AnimData.IdleAnimation, AnimData.DefaultLayer);
        }

        public static void PlayAttackMeleeMob(RAAnimationEntity entity, Animator animator)
        {
            animator.SetTrigger(AnimData.AttackTrigger);

            entity.isAttackTrigger = false;
        }

        public static void PlayAttackGunnerMob(RAAnimationEntity entity, Animator animator)
        {
            animator.SetTrigger(AnimData.AttackTrigger);
        }

        public static void PlayDie(RAAnimationEntity entity, Animator animator)
        {
            if (animator.GetLayerName(AnimData.RunningLayer)!= null)
            {
                animator.SetLayerWeight(AnimData.RunningLayer, 0);    
            }

            
            animator.SetTrigger(AnimData.DieTrigger);

            entity.isDieTrigger = false;
        }

        public static void PlaySitAnimation(RAAnimationEntity entity, Animator animator)
        {
            animator.SetBool(AnimData.IsSit, entity.hasIsSit && entity.isSit.Value);
        }

        public static void PlayHitDamage(RAAnimationEntity entity, Animator animator)
        {
            animator.SetTrigger(AnimData.HitTrigger);

            entity.isHitTrigger = false;
        }
    }
}