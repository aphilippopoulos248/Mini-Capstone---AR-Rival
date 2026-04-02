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

    protected override Status OnStart()
    {
        Self.Value.Die();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}
