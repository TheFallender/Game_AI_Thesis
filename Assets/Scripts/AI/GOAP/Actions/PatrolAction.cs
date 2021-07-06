using UnityEngine;
using SGoap;

//Patrol Action:
//As doing a patrol can result in an infinite loop,
//this is not going to use the goto action, because
//it could cause the agent to plan forever
public class PatrolAction : MainAction {
    //System object
    [SerializeField] private Patrol patrol;

    //Range for patrols
    [SerializeField] private float rangeWaypoints;

    //Get target info
    [SerializeField] private StringReference hasTarget;

    //Perform on Awake
    private void Awake () {
        patrol.Initialize();
    }

    //Perform before the action
    public override bool PrePerform () {
        //Set last waypoint as the current one
        SetWaypointDestination(patrol.GetCurrentWaypoint());
        return true;
    }

    //Perform the action
    public override EActionStatus Perform () {
        //If it reached the point, set the next one
        if (AgentData.goTo.ReachedDestination()) {
            //If it has already waited or there is no wait
            if (!patrol.NeedToWait())
                //Get the next waypoint info
                SetWaypointDestination(patrol.FindNextWaypoint());
            //If it's not currently waiting
            else {
                patrol.WaitingOnWaypoint();
                AgentData.goTo.StopPathfinding();
            }
        }

        //Return value based on the target status from the states
        if (!States.HasState(hasTarget.ToString()))
            return EActionStatus.Running;
        else
            return EActionStatus.Success;
    }

    //Set the waypoint destination
    private void SetWaypointDestination (Waypoint waypointToGo) {
        AgentData.goTo.GoToPoint(waypointToGo.transform, rangeWaypoints);
    }

    //Post perform
    public override bool PostPerform () {
        return true;
    }
}
