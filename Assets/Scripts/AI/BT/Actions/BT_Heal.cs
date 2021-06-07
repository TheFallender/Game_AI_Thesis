using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Heal Action")]
public class BT_Heal : BT_StateAction {
    //Heal reference
    [SerializeField] private SharedBool hasHeal;

    //Target reference
    [SerializeField] private SharedBool hasTarget;

    //Health low reference
    [SerializeField] private SharedInt lowHealth;

    //OnUpdate Call
    public override TaskStatus OnUpdate () {
        //Use the heal
        agentData.Value.availableHeal.UseHeal(agentData.Value.health);
        agentData.Value.availableHeal = null;

        //Reset health
        if (!agentData.Value.health.IsHealthLow())
            lowHealth.Value = 0;

        //Remove heal
        hasHeal.Value = false;

        //Remove target
        hasTarget.Value = false;

        return TaskStatus.Success;
    }
}