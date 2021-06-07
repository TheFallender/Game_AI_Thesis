using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Flee Action")]
public class BT_Flee : BT_StateAction {
    //Flee System
    [SerializeField] private Flee flee;

    //References
    [SerializeField] private SharedBool shouldFlee;

    //Range for flees
    [SerializeField] private float fleeRange;

    //OnUpdate Call
    public override TaskStatus OnUpdate () {
        //If it must flee
        if (flee.MustItFlee()) {
            //If it has reached the destination, disable the current wait
            if (agentData.Value.goTo.ReachedDestination())
                flee.StopFleeWait();

            //If it has finished fleeing get a new 
            if (flee.fleeFinished)
                agentData.Value.goTo.GoToPoint(flee.GetNewFlee(), fleeRange);

            return TaskStatus.Running;
        } else {
            shouldFlee.Value = false;

            //Reset the heals
            flee.ResetCounter();

            return TaskStatus.Success;
        }
    }
}