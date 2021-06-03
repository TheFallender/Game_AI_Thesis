using UnityEngine;
using SGoap;
using Pathfinding;

public class Old_GoToAction : BasicAction {
    //Agent Parameters
    public Transform target;                //Target of the Agent
    public StringReference stateToUpdate;   //State to update about the distance
    private float rangeOfAction = -1f;      //Range of the action to perform

    //Pathfinding components
    public GameObject pathfindingObject;//Pathfinding Object
    private Seeker seeker;              //Seeker of the target
    private AIPath aiPath;              //AI path moving
    private AIDestinationSetter destStr;//Setter to target
    private bool runningPath = false;   //Running a path already

    //Set bases on start
    public void Start () {
        //Get the components of the pathfinding
        seeker = pathfindingObject.GetComponent<Seeker>();
        aiPath = pathfindingObject.GetComponent<AIPath>();
        destStr = pathfindingObject.GetComponent<AIDestinationSetter>();

        //Set the target
        ChangeTarget(target, true);
    }

    //Pre Perform
    public override bool PrePerform () {
        RangedAction.GetNextRangedAction(AgentData.Agent.ActionQueue);
        return base.PrePerform();
    }

    //Perform the action
    public override EActionStatus Perform () {
        //Check if the target is in range
        if (!IsInRange()) {
            //Only run if there is no current pathfinding
            if (!runningPath)
                PathFinding();

            //Action Running
            return EActionStatus.Running;
        } else {
            //Only stop if there is a pathfinding
            if (runningPath)
                StopPathfinding();

            //Action Completed
            return EActionStatus.Success;
        }
    }

    //Check if the target is in range
    private bool IsInRange () {
        //Calculate distance
        float distance = Vector3.Distance(
            AgentData.Position,
            target.position
        );

        //Update the state
        AgentData.Agent.States.SetState(stateToUpdate.ToString(), distance);

        //Return whether the target is in distance or not
        return distance < rangeOfAction;
    }

    //Pathfinding to position
    private void PathFinding () {
        //Avoid calling pathfinding multiple times
        runningPath = true;

        //Allow movement
        aiPath.canMove = true;

        //Set the range wanted
        aiPath.endReachedDistance = (float) rangeOfAction;

        //Pathfind to target position, when it completes call function
        seeker.StartPath(transform.position, target.position);
    }

    //Stop or cancel the pathfinding
    public void StopPathfinding () {
        //Disable the running pathfinding check
        runningPath = false;

        //Disable movement
        aiPath.canMove = false;
    }

    //Change target of agent
    public void ChangeTarget (Transform newTarget, bool changeMainTarget = false) {
        //Main target
        target = newTarget;

        //Target of the agent
        if (changeMainTarget)
            AgentData.Target = newTarget;

        //Target of the destination setter
        destStr.target = newTarget;
    }
}