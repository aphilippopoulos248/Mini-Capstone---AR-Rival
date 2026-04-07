using UnityEngine;

public class EnemyIdleState : FSMBaseState
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
            controller.isAttacking = false;
        }
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
