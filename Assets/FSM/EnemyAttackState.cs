using UnityEngine;

public class EnemyAttackState : FSMBaseState
{
    float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer < controller.attackCoolDown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            animator.ResetTrigger("Attack");
        }
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        controller.isAttacking = false;
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
