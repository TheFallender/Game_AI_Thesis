using SGoap;
using UnityEngine;

public class AttackAction : MainAction {

    //Attack System
    [SerializeField] private Attack attack;

    //Has target
    [SerializeField] private StringReference hasTarget;

    //Attack range
    [SerializeField] private float rangeOfAttacks;

    //PrePerform
    public override bool PrePerform () {
        //Assign the target
        attack.AssignTarget(AgentData.Target);

        //Set the target for the goto
        AgentData.goTo.GoToPoint(AgentData.Target, rangeOfAttacks);
        return true;
    }

    //Perform
    public override EActionStatus Perform () {
        //The agent will continue to follow the target and
        //hit it until it's either dead or the agent has
        //taken enough damage to retreat or be dead
        if (!AgentData.goTo.TargetExists() || !attack.TargetAlive()) {
            attack.RemoveTarget();
            States.RemoveState(hasTarget.ToString());
            return EActionStatus.Success;
        } else {
            //Keep pointing at the target if it is close enough
            AgentData.goTo.PointToObject();
            return EActionStatus.Running;
        }
    }

    //PostPerform
    public override bool PostPerform () {
        return true;
    }
}