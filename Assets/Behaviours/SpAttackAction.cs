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
    float timer;
    protected override Status OnStart()
    {
        timer = 0;
        Self.Value.Attack(true);
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
        if (timer < 1f)
        {
            timer += Time.deltaTime;
            return Status.Running;
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
        Self.Value.ResetStatus();
    }
}
