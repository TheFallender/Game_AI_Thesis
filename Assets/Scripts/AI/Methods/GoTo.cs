using UnityEngine;
using Pathfinding;

public class GoTo : MonoBehaviour
{
    //GoTo Variables
    private Transform target;                       //Target of the Agent
    private float rangeOfAction = -1f;              //Range of the action to perform
    [SerializeField] private float rotationSpeed;   //Rotation speed

    //Pathfinding components
    public GameObject pathfindingObject;    //Pathfinding Object
    private Seeker seeker;                  //Seeker of the target
    private AIPath aiPath;                  //AI path moving
    private AIDestinationSetter destStr;    //Setter to target

    //Set variable components on start
    private void Awake () {
        //Get the components of the pathfinding
        seeker = pathfindingObject.GetComponent<Seeker>();
        aiPath = pathfindingObject.GetComponent<AIPath>();
        destStr = pathfindingObject.GetComponent<AIDestinationSetter>();
    }

    //Go to the specified point
    public void GoToPoint (Transform newTarget, float rangeNeeded) {
        target = newTarget;
        rangeOfAction = rangeNeeded;

        //Check if the target is in range
        if (!ObjectNearEnough(pathfindingObject.transform, target, rangeOfAction)) {
            PathFinding();
        } else {
            StopPathfinding();
        }
    }

    //Point towards the current target
    public void PointToObject () {
        PointToObject(target);
    }

    //Point towards an specific target
    public void PointToObject (Transform targetToPoint) {
        //Create a normalized vector pointing in the direction of the target
        Vector3 targetDir = (targetToPoint.position - pathfindingObject.transform.position).normalized;
        
        //Only allow Y angle movement
        targetDir.Scale(new Vector3(1, 0, 1));

        //Create the quaternion
        Quaternion whereToLook = Quaternion.LookRotation(targetDir);

        //Perform the rotation
        pathfindingObject.transform.rotation = Quaternion.Slerp(pathfindingObject.transform.rotation, whereToLook, Time.deltaTime * rotationSpeed);
    }

    //Get target
    public Transform GetTarget () {
        return target;
    }

    //Pathfinding to position
    private void PathFinding () {
        //Allow movement
        aiPath.canMove = true;

        //Set the range wanted
        aiPath.endReachedDistance = (float) rangeOfAction;

        //Target of the destination setter
        destStr.target = target;

        //Pathfind to target position
        seeker.StartPath(pathfindingObject.transform.position, target.position);
    }

    //Stop or cancel the pathfinding
    public void StopPathfinding () {
        //Disable movement
        aiPath.canMove = false;
    }

    //Distance to the point specified
    public static float DistanceToPoint (Transform origin, Transform destination) {
        return Vector3.Distance(origin.position, destination.position);
    }

    //Check if the object is near enough
    public static bool ObjectNearEnough (Transform origin, Transform destination, float range) {
        return DistanceToPoint(origin, destination) <= range;
    }

    //Return the current distance to a target
    public float DistanceToTarget () {
        return DistanceToPoint(pathfindingObject.transform, target);
    }

    //Check if the destination has been reached
    public bool ReachedDestination () {
        return aiPath.reachedDestination;
    }

    //Target exists: Check if the target still exists
    public bool TargetExists () {
        try {
            target.gameObject.GetInstanceID();
            return true;
        } catch {
            return false;
        }
    }
}
