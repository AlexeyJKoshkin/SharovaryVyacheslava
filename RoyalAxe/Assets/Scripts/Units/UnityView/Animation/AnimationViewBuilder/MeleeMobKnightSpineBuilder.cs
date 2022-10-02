using System;

namespace RoyalAxe.Units
{
    [Serializable]
    public class MeleeMobKnightSpineBuilder : AnimationEnemyBuilder
    {
        protected override void AddAnimationData(RAAnimationEntity animationEntity)
        {
            animationEntity.AddRun(true);
        }
    }
}