using UnityEngine;

public class EnemyAttackState : FSMBaseState
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller.isAttacking = true;
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
