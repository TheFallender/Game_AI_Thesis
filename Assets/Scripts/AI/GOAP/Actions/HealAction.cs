using UnityEngine;
using SGoap;

public class HealAction : MainAction {
    //Heal reference
    [SerializeField] private StringReference hasHeal;

    //Health low reference
    [SerializeField] private StringReference lowHealth;

    //Target reference
    [SerializeField] private StringReference hasTarget;


    //PrePerform
    public override bool PrePerform () {
        if (!AgentData.AvailableHeal)
            return false;
        return true;
    }

    //Perform
    public override EActionStatus Perform () {
        //Remove heal
        States.RemoveState(hasHeal.ToString());

        //Remove target
        States.RemoveState(hasTarget.ToString());

        //Use the heal
        AgentData.AvailableHeal.UseHeal(AgentData.health);

        if (!AgentData.health.IsHealthLow()) {
            States.SetState(
                lowHealth.ToString(),
                0f
            );
        }

        //Delete the heal variables
        AgentData.AvailableHeal = null;

        return EActionStatus.Success;
    }

    //PostPerform
    public override bool PostPerform () {
        return true;
    }
}