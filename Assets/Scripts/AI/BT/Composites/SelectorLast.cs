using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Same as selector but will run the last child regardless of the success or failure on the previous childs.")]
[TaskIcon("{SkinColor}SelectorIcon.png")]
public class SelectorLast : Composite {
    //Index of the current children to run
    private int currentChildIndex = 0;

    //Status of the last child that run
    private TaskStatus executionStatus = TaskStatus.Inactive;

    //Get the current index
    public override int CurrentChildIndex () {
        return currentChildIndex;
    }

    //Can the next child be executed?
    public override bool CanExecute () {
        //While there are childrens to run
        if (currentChildIndex < children.Count) {
            //If the last child run succeeded and it is not the last one in the branch
            if (executionStatus == TaskStatus.Success && currentChildIndex != children.Count - 1) {
                //Move to the final branch
                currentChildIndex = children.Count - 1;
            }

            //Execute the next child
            //or last one if it was failure
            return true;
        }
        return false;
    }

    //Return the status of the previous childs, not the status of the last one
    public override TaskStatus OverrideStatus (TaskStatus status) {
        return executionStatus;
    }

    //Check to do when a child is executed
    public override void OnChildExecuted (TaskStatus childStatus) {
        //Increase child index
        currentChildIndex++;

        //If the last children run is not the last one
        //or it is the only one
        if (children.Count == 1 || currentChildIndex != children.Count)
            executionStatus = childStatus;
    }

    //When the composite finishes
    public override void OnEnd () {
        //Reset children and run status.
        executionStatus = TaskStatus.Inactive;
        currentChildIndex = 0;
    }
}
