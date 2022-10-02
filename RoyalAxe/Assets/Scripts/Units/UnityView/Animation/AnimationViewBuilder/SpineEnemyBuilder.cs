using System;
using Entitas;
using UnityEngine;

namespace RoyalAxe.Units
{
    /// <summary>
    ///     настраивает спайн анимацию юнита. добавляет необходимые компоненты триггеры
    /// </summary>
    [Serializable]
    public abstract class AnimationEnemyBuilder : IAnimationUnitViewBuilder
    {
        [SerializeField] private Animator _animator;

        public void InitEntity(IEntity entity)
        {
            if (_animator == null)
            {
                return;
            }

            if (entity is UnitsEntity unit)
            {
                var animationEntity = unit.unitAnimationEntity.AnimationEntity;
                animationEntity.AddAnimator(_animator);
                AddAnimationData(animationEntity);
            }
        }

        protected abstract void AddAnimationData(RAAnimationEntity animationEntity);
    }
}