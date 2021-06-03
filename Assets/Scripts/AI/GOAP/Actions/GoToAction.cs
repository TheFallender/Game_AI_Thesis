using UnityEngine;
using SGoap;

public class GoToAction : MainAction {
    //Next ranged action to perform
    private RangedAction nextRangedAction;

    //Perform before the action
    public override bool PrePerform () {
        //Set destination
        nextRangedAction = RangedAction.GetNextRangedAction(AgentData.Agent.ActionQueue);

        //Go to destination
        AgentData.goTo.GoToPoint(nextRangedAction.target, nextRangedAction.rangeNeeded);

        //Set target
        AgentData.Target = nextRangedAction.target;

        return true;
    }

    //Perform the action
    public override EActionStatus Perform () {
        //Check if the target is in range
        if (!AgentData.goTo.ReachedDestination())
            return EActionStatus.Running;
        else
            return EActionStatus.Success;
    }

    //Perform after ending the action
    public override bool PostPerform () {
        //Update the state
        AgentData.Agent.States.SetState(
            nextRangedAction.stateToUpdate.ToString(),
            AgentData.goTo.DistanceToTarget()
        );
        return true;
    }
}
