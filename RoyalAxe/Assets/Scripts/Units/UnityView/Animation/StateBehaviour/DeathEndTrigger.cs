using RoyalAxe.Units.UnitBehaviour;
using UnityEngine;

namespace RoyalAxe
{
    public class DeathEndTrigger : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            animator.GetComponent<AbstractUnitBehaviour>().OnDeathEnd();
        }
    }
}