using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Attack Action")]
public class BT_Attack : BT_StateAction {
    //Attack System
    [SerializeField] private Attack attack;

    //Has target
    [SerializeField] private SharedBool hasTarget;

    //Attack range
    [SerializeField] private float rangeOfAttacks;

    //OnUpdate Call
    public override TaskStatus OnUpdate () {
        //To allow for a first entry execution
        if (!CurrentStateSet()) {
            SetState();

            //Assign the target
            attack.AssignTarget(agentData.Value.target);

            //Set the target for the goto
            agentData.Value.goTo.GoToPoint(agentData.Value.target, rangeOfAttacks);
        }

        //The agent will continue to follow the target and
        //hit it until it's either dead or the agent has
        //taken enough damage to retreat or be dead
        if (!agentData.Value.goTo.TargetExists() || !attack.TargetAlive()) {
            attack.RemoveTarget();
            hasTarget.Value = false;
        } else {
            //Keep pointing at the target if it is close enough
            agentData.Value.goTo.PointToObject();
        }
        return TaskStatus.Success;
    }
}
