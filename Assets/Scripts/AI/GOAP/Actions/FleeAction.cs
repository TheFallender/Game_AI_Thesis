using UnityEngine;
using SGoap;

public class FleeAction : MainAction {
    //Flee Action
    [SerializeField] private Flee flee;

    //References
    [SerializeField] private StringReference shouldItFlee;

    //Range for flees
    [SerializeField] private float fleeRange;

    //PrePerform
    public override bool PrePerform () {
        return true;
    }

    //Perform
    public override EActionStatus Perform () {
        //If it must flee
        if (flee.MustItFlee()) {
            //If it has reached the destination, disable the current wait
            if (AgentData.goTo.ReachedDestination())
                flee.StopFleeWait();

            //If it has finished fleeing get a new 
            if (flee.fleeFinished)
                AgentData.goTo.GoToPoint(flee.GetNewFlee(), fleeRange);

            return EActionStatus.Running;
        } else {
            States.RemoveState(shouldItFlee.ToString());

            //Reset the heals
            flee.ResetCounter();

            return EActionStatus.Success;
        }
    }

    //PostPerform
    public override bool PostPerform () {
        return true;
    }
}