using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Die", story: "[Self] Die", category: "Action", id: "d8e68e8c0c1b96f5d64ed97404672bb3")]
public partial class DieAction : Action
{
    [SerializeReference] public BlackboardVariable<BossCombat> Self;
    private Animator animator;
    private readonly int ANIM_DIE = Animator.StringToHash("Dead");

    protected override Status OnStart()
    {
        Self.Value.Die();
        animator = Self.Value.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(ANIM_DIE);
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Self.Value.agent.GetVariable<bool>("isDead", out var shouldDie) == false)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd()
    {
        Debug.Log("DieAction ended");
    }
}
