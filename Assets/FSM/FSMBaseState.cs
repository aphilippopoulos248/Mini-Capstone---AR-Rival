using UnityEngine;

public class FSMBaseState : StateMachineBehaviour
{
    protected EnemyController controller {get; set;}
    protected Animator animator {get; set;}

    public void Init(EnemyController _controller, Animator _animator)
    {
        controller = _controller;
        animator = _animator;
    }
}
