using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Patrol Action")]
public class BT_Patrol : BT_StateAction {
    //System object
    [SerializeField] private Patrol patrol;

    //Range for patrols
    [SerializeField] private float rangeWaypoints;

    //Shared variable of the target status
    [SerializeField] private SharedBool hasTarget;

    //Initialize the patrol
    public override void OnAwake () {
        patrol.Initialize();
    }

    //When it is updated
    public override TaskStatus OnUpdate () {
        //To allow for a first entry execution
        if (!CurrentStateSet()) {
            SetState();

            //Set last waypoint as the current one
            SetWaypointDestination(patrol.GetCurrentWaypoint());
        }


        //If it reached the point, set the next one
        if (agentData.Value.goTo.ReachedDestination()) {
            //If it has already waited or there is no wait
            if (!patrol.NeedToWait())
                //Get the next waypoint info
                SetWaypointDestination(patrol.FindNextWaypoint());
            //If it's not currently waiting
            else {
                patrol.WaitingOnWaypoint();
                agentData.Value.goTo.StopPathfinding();
            }
        }


        return TaskStatus.Running;


        ////Return value based on the target status from the states
        //if (hasTarget.Value)
        //    return TaskStatus.Running;
        //else
        //    return TaskStatus.Success;
    }

    //Set the waypoint destination
    private void SetWaypointDestination (Waypoint waypointToGo) {
        agentData.Value.goTo.GoToPoint(waypointToGo.transform, rangeWaypoints);
    }
}
