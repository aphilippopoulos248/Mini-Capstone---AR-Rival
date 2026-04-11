using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SpAttack", story: "[Self] SpAttack", category: "Action", id: "7f47d3566e2ffc40d62a4f40678f39f0")]
public partial class SpAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<BossCombat> Self;
    [SerializeReference] public BlackboardVariable<float> AnimationDuration;
    private Animator animator;
    private readonly int ANIM_ATTACK = Animator.StringToHash("Attacking");
    private readonly int ANIM_ENRAGED = Animator.StringToHash("Enraged");

    float cdTimer;
    float animationTimer;

    protected override Status OnStart()
    {
        cdTimer = 0;
        animationTimer = 0;
        Self.Value.Attack(true);
        animator = Self.Value.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool(ANIM_ATTACK, true);
            animator.SetBool(ANIM_ENRAGED, true);
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
            animator.SetBool(ANIM_ENRAGED, false);
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
        animator = Self.Value.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool(ANIM_ATTACK, false);
            animator.SetBool(ANIM_ENRAGED, false);
        }
    }
}
