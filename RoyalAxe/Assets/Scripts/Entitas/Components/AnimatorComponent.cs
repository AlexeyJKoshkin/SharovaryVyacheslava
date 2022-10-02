using Entitas;
using UnityEngine;

namespace RoyalAxe.GameEntitas
{
    public abstract class AnimationParamValue<T>
    {
        public T Value;
    }

    public abstract class BoolAnimationParamValue : AnimationParamValue<bool> { }

    [RAAnimation]
    public class AnimatorComponent : IComponent
    {
        public Animator Controller;
    }

    [RAAnimation]
    public class DieTriggerComponent : IComponent { }

    [RAAnimation]
    public class AttackTriggerComponent : IComponent { }

    [RAAnimation]
    public class HitTriggerComponent : IComponent { }

    [RAAnimation]
    public class IsSitComponent : BoolAnimationParamValue, IComponent { }

    [RAAnimation]
    public class RunComponent : BoolAnimationParamValue, IComponent { }
}