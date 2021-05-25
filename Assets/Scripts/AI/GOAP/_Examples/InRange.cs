using UnityEngine;
using SGoap;

namespace ImplementationExamples {
    namespace GOAP {
        //GOAP InRange Example
        public class InRange : BasicAction {
            //Agent Parameters
            public Transform target;    //Target of the Agent
            public float rangeOfAgent;  //Range of the Agent

            //Pathfinding
            public GameObject pathfindingObject;//Pathfinding Object
            private Pathfinding.Seeker seeker;  //Seeker of the target
            private Pathfinding.AIPath aiPath;  //AI path moving
            private bool runningPath = false;   //Running a path already


            //Set bases on start
            public void Start () {
                //Set the target
                AgentData.Target = target;

                //Get the components of the pathfinding
                seeker = pathfindingObject.GetComponent<Pathfinding.Seeker>();
                aiPath = pathfindingObject.GetComponent<Pathfinding.AIPath>();

                //Set the target to the pathfinder
                pathfindingObject.GetComponent<Pathfinding.AIDestinationSetter>().target = target;
            }

            //Action to perform
            public override EActionStatus Perform () {
                //Calculate distance to target
                float distance = Vector3.Distance(
                    AgentData.Position,
                    target.position
                );

                //Check if it is within range
                if (distance <= rangeOfAgent) {
                    //Check if there was a path setted
                    if (runningPath) {
                        //Disable the path
                        aiPath.canMove = false;
                        runningPath = false;
                    }

                    //Target in range, proceed to next action
                    return EActionStatus.Success;
                } else {
                    //Check if there is a path already running
                    if (!runningPath) {
                        //Set the path and the 
                        seeker.StartPath(transform.position, target.position);
                        aiPath.canMove = true;
                        runningPath = true;
                    }

                    //Wait until the target is in range
                    return EActionStatus.Running;
                }
            }
        }
    }
}