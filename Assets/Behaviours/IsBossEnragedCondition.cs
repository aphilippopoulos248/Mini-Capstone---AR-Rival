using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsBossEnraged", story: "Check if boss [IsEnraged]", category: "Conditions", id: "ecde48b2e17d1b8247e4efa344a87d6d")]
public partial class IsBossEnragedCondition : Condition
{
    [SerializeReference] public BlackboardVariable<bool> IsEnraged;
    public override bool IsTrue()
    {
        return IsEnraged.Value == true ? true : false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
