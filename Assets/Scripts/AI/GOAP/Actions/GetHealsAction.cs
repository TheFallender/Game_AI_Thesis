using UnityEngine;
using SGoap;

public class GetHealsAction : MainAction {
    //Currently wanted heal
    private Heal healWanted;

    //Flee system to update
    [SerializeField] private Flee flee;
    
    //Should it flee?
    [SerializeField] private StringReference shouldFlee;

    //PrePerform
    public override bool PrePerform () {
        return CheckForHeals();
    }

    //Perform
    public override EActionStatus Perform () {
        //If it has a heal, go to the healing state
        if (AgentData.AvailableHeal != null) {
            healWanted = null;
            return EActionStatus.Success;
        } else if (!AgentData.goTo.TargetExists() || healWanted.hasBeenTaken) {
            //If the heal has been taken or it has been consumed
            //recheck for more heals
            return CheckForHeals() ?
                EActionStatus.Running :
                EActionStatus.Failed;
        } else
            return EActionStatus.Running;
    }

    //PostPerform
    public override bool PostPerform () {
        return true;
    }

    private bool CheckForHeals () {
        //Find nearby heals
        healWanted = Heal.FindHealNearby(transform);

        //Agent in panic as there are no heals nearby
        if (healWanted == null) {
            //Flee forever
            flee.FleeForever();

            //Set that the agent has to flee
            States.SetState(
                shouldFlee.ToString(),
                1f
            );

            //Abort the current plan
            AgentData.Agent.ForceReplan();

            return false;
        } else {
            AgentData.goTo.GoToPoint(healWanted.transform, 0f);
            return true;
        }
    }
}
