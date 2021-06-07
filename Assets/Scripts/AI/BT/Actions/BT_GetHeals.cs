using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("GetHeals Action")]
public class BT_GetHeals : BT_StateAction {
    //Currently wanted heal
    private Heal healWanted;

    //Flee system to update
    [SerializeField] private Flee flee;

    //Should it flee?
    public SharedBool shouldFlee;

    //OnUpdate Call
    public override TaskStatus OnUpdate () {
        //First entry execution
        if (!CurrentStateSet()) {
            SetState();

            return CheckForHeals() ?
                TaskStatus.Success :
                TaskStatus.Failure;
        }

        //Consecutive executions
        /* CODE TO RUN */
        //If it has a heal, go to the healing state
        if (agentData.Value.availableHeal != null) {
            healWanted = null;
            return TaskStatus.Success;
        } else if (!healWanted || !agentData.Value.goTo.TargetExists() || healWanted.hasBeenTaken) {
            //If the heal has been taken or it has been consumed
            //recheck for more heals
            return CheckForHeals() ?
                TaskStatus.Success :
                TaskStatus.Failure;
        } else
            return TaskStatus.Success;
    }

    private bool CheckForHeals () {
        //Find nearby heals
        healWanted = Heal.FindHealNearby(agentData.Value.goTo.pathfindingObject.transform);

        //Agent in panic as there are no heals nearby
        if (healWanted == null) {
            //Flee forever
            flee.FleeForever();

            //Set that the agent has to flee
            shouldFlee.Value = true;

            return false;
        } else {
            agentData.Value.goTo.GoToPoint(healWanted.transform, 0f);
            return true;
        }
    }
}