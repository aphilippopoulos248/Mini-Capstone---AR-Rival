using UnityEngine;

public class EnemyStunState : FSMBaseState
{
    float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer < controller.stunTimeSpan)
        {
            timer += Time.deltaTime;
        }
        else
        {
            animator.SetBool("Stunned", false);
        }
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
