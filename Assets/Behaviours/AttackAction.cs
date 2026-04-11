using System;
using Unity.AppUI.MVVM;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Self] Attack", category: "Action", id: "ba6a2b07f7d71e3c2b2c985b35f8e677")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<BossCombat> Self;
    [SerializeReference] public BlackboardVariable<float> AnimationDuration;
    private Animator animator;
    private readonly int ANIM_ATTACK = Animator.StringToHash("Attacking");

    float cdTimer;
    float animationTimer;

    protected override Status OnStart()
    {
        cdTimer = 0;
        animationTimer = 0;
        Self.Value.Attack();
        animator = Self.Value.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool(ANIM_ATTACK, true);
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (
            Self.Value.agent.GetVariable<bool>("isDead", out var shouldDie) &&
            Self.Value.agent.GetVariable<bool>("isStunned", out var shouldStun) &&
            (
                shouldStun.Value == true ||
                shouldDie.Value == true
            )
        )
        {
            return Status.Success;
        }
        if (animationTimer < AnimationDuration)
        {
            animationTimer += Time.deltaTime;
            return Status.Running;
        }
        else if (cdTimer < Self.Value.GetCooldown())
        {
            animator.SetBool(ANIM_ATTACK, false);
            if (cdTimer == 0)
            {
                Self.Value.ResetStatus();
            }
            cdTimer += Time.deltaTime;
            return Status.Running;
        }
        else
        {        
            return Status.Success;
        }
    }

    protected override void OnEnd()
    {
        if (animator != null)
        {
            animator.SetBool(ANIM_ATTACK, false);
        }
    }
}

