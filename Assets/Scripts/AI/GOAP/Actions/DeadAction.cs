using UnityEngine;
using SGoap;

public class DeadAction : MainAction {
    [SerializeField] private GameObject mainObject;

    //PrePerform
    public override bool PrePerform () {
        return true;
    }

    //Perform
    public override EActionStatus Perform () {
        if (AgentData.AvailableHeal)
            AgentData.AvailableHeal.DeleteHeal();

        Destroy(mainObject);

        return EActionStatus.Success;
    }

    //PostPerform
    public override bool PostPerform () {
        return true;
    }
}
