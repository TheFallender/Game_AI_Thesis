using System.Collections;
using UnityEngine;

//Patrol System for the AI
public class Patrol : MonoBehaviour{
    //Private variables
    private int routePos = 0;                       //Current position in the route
    private bool currentlyWaiting = false;          //Check if it's waiting
    private bool waitingDone = false;               //Checek if the waiting has finished 

    //Defined by user on the inspector
    [SerializeField] private bool loopPatrol = false;       //Should it loop on the patrol?
    [SerializeField] private bool goingBackwards = false;   //Is it going backwards (from waypoint n to waypoint 0)
    [SerializeField] private Waypoint currentWaypoint;      //Current Waypoint asigned
    [SerializeField] private Route routeToFollow;           //Route to follow

    //Determine which one should be the first waypoint
    public void Initialize () {
        //Initialize the route
        routeToFollow.Initialize();

        //If no currenWaypoint is defined, select the first one of the route
        if (currentWaypoint == null)
            currentWaypoint = routeToFollow.GetFirstWaypoint(ref routePos);
        else
            routePos = routeToFollow.GetWaypointIndex(currentWaypoint);
    }

    //Find the next waypoint
    public Waypoint FindNextWaypoint () {
        //Find the next waypoint based on the direction it wants to follow
        if (!goingBackwards)
            currentWaypoint = routeToFollow.GetNextWaypoint(ref routePos);
        else
            currentWaypoint = routeToFollow.GetPreviousWaypoint(ref routePos);

        //If it's not looping, check if it should turn back
        if (!loopPatrol) {
            if (currentWaypoint.shouldTurnBack)
                goingBackwards = !goingBackwards;
        }

        //Reset waypoint done
        waitingDone = false;

        return currentWaypoint;
    }

    //In case you want to get the current waypoint without going for the next one
    public Waypoint GetCurrentWaypoint () {
        return currentWaypoint;
    }

    //Does it have to wait?
    public bool NeedToWait () {
        return currentWaypoint.waitOnWaypoint > 0f && !waitingDone;
    }

    //Start To Wait on the waypoint
    public void WaitingOnWaypoint () {
        if (!currentlyWaiting)
            StartCoroutine(WaitOnWaypoint());
    }

    //Wait on the waypoint
    private IEnumerator WaitOnWaypoint () {
        currentlyWaiting = true;
        yield return new WaitForSeconds(currentWaypoint.waitOnWaypoint);
        currentlyWaiting = false;
        waitingDone = true;
    }
}