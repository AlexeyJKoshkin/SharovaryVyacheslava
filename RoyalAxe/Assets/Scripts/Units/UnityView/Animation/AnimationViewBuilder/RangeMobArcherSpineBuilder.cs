using System;

namespace RoyalAxe.Units
{
    [Serializable]
    public class RangeMobArcherSpineBuilder : AnimationEnemyBuilder
    {
        protected override void AddAnimationData(RAAnimationEntity animationEntity)
        {
            animationEntity.AddIsSit(true);
        }
    }
}