using UnityEngine;

//Waypoint: it contains information for the agent
public class Waypoint : MonoBehaviour {
    //Allows the agent to wait on the point
    public float waitOnWaypoint = 0f;

    //Determines if the agent should go the route backwards
    public bool shouldTurnBack = false;

    //Point to the ground
    public static bool SetPointOnTheGround (Transform point, float range) {
        //Raycast from the point plus a little above to find if there is ground below it
        if (Physics.Raycast(point.transform.position + new Vector3(0, 0.1f), Vector3.down, out RaycastHit rayHit, range)) {
            point.position = rayHit.point;
            return true;
        } else {
            return false;
        }
    }
}