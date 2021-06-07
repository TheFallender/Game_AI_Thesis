using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Will execute all the childs regardless of their success or failure. Will return an AND of the child's results.")]
[TaskIcon("{SkinColor}ParallelIcon.png")]
public class Execute : Composite {
    //Index of the current children to run
    private int currentChildIndex = 0;

    //Status of the last child that run
    private TaskStatus executionStatus = TaskStatus.Inactive;

    //Status of all the childrens applied as an AND
    private TaskStatus globalStatus = TaskStatus.Failure;

    //Get the current index
    public override int CurrentChildIndex () {
        return currentChildIndex;
    }

    //Can the next child be executed?
    public override bool CanExecute () {
        //As long as there are childrens to run, execute
        return currentChildIndex < children.Count;
    }

    //Return the status of the previous childs
    public override TaskStatus OverrideStatus (TaskStatus status) {
        //If there is no current task running
        if (executionStatus != TaskStatus.Inactive ||
            executionStatus != TaskStatus.Running) {
            //Return the global status
            return globalStatus;
        } else
            //Return the child status
            return executionStatus;
    }

    //Check to do when a child is executed
    public override void OnChildExecuted (TaskStatus childStatus) {
        //Increase child index
        currentChildIndex++;

        //Apply the last status
        executionStatus = childStatus;

        //As soon as one fails return
        if (childStatus == TaskStatus.Failure)
            globalStatus = childStatus;
    }

    //When the composite finishes
    public override void OnEnd () {
        //Reset children and run status.
        executionStatus = TaskStatus.Inactive;
        globalStatus = TaskStatus.Failure;
        currentChildIndex = 0;
    }
}
