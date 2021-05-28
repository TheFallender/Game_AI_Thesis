using System.Collections.Generic;
using UnityEngine;

//Route: It will tell an agent which waypoints to use
public class Route : MonoBehaviour {
    //List of waypoints available
    //Did not use a linked list due to the performance costs
    public List<Waypoint> waypoints;

    //Max distance to check below a waypoint
    const float maxDistanceFromWaypoint = 10f;

    //Has been initialized
    private bool hasBeenInitialized = false;

    //Call the initialization only once
    private void Awake () {
        Initialize();
    }

    public void Initialize () {
        //Only continue if it has not been initialized
        if (hasBeenInitialized)
            return;

        //Add the child waypoints
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            waypoints.Add(child.GetComponent<Waypoint>());
        }

        //Set the waypoints on the ground for easier pathfinding
        SetWaypointsOnTheGround();

        //Initialization finished
        hasBeenInitialized = true;
    }

    //To avoid issues with the waypo
    private void SetWaypointsOnTheGround () {
        foreach (Waypoint point in waypoints) {
            Waypoint.SetPointOnTheGround(point.transform, maxDistanceFromWaypoint);
        }
    }

    //Get the first waypoint on the list
    public Waypoint GetFirstWaypoint (ref int currentPos) {
        currentPos = 0;
        return waypoints[currentPos];
    }

    //Get the last waypoint on the list
    public Waypoint GetLastWaypoint (ref int currentPos) {
        currentPos = waypoints.Count - 1;
        return waypoints[currentPos];
    }

    //Get the next waypoint
    public Waypoint GetNextWaypoint (ref int currentPos) {
        currentPos++;
        currentPos = ModuleThePosition(ref currentPos);
        return waypoints[currentPos];
    }

    //Get the previous waypoint
    public Waypoint GetPreviousWaypoint (ref int currentPos) {
        currentPos--;
        currentPos = ModuleThePosition(ref currentPos);
        return waypoints[currentPos];
    }

    //Get the index of a given waypoint
    public int GetWaypointIndex (Waypoint waypointToFind) {
        return waypoints.FindIndex(waypoint => 
            waypoint.gameObject.GetInstanceID() == waypointToFind.gameObject.GetInstanceID()
        );
    }

    //Reset the counter based on the number of waypoints
    public int ModuleThePosition (ref int currentPos) {
        if (currentPos >= waypoints.Count)
            currentPos = 0;
        else if (currentPos < 0)
            currentPos = waypoints.Count - 1;
        return currentPos;
    }
}
