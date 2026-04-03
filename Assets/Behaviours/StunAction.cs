using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stun", story: "[Self] Stun", category: "Action", id: "5cdd5d39d609d765bccda63d2b1948ba")]
public partial class StunAction : Action
{
    [SerializeReference] public BlackboardVariable<BossCombat> Self;
    float timer;

    protected override Status OnStart()
    {
        timer = 0;
        Self.Value.Stun();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (
            Self.Value.agent.GetVariable<bool>("isDead", out var shouldDie) &&
            (
                shouldDie.Value == true
            )
        )
        {
            return Status.Success;
        }
        if (timer < Self.Value.GetStunTimeSpan())
        {
            timer += Time.deltaTime;
            return Status.Running;
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
        Self.Value.agent.SetVariableValue<bool>("isStunned", false);
        Self.Value.ResetStatus();
    }
}
