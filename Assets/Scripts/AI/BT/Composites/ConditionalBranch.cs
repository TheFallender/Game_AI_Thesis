using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Branch to the left or right based on if it is true or false (true: left and false: right)")]
[TaskIcon("{SkinColor}Conditional.png")]
public class ConditionalBranch : Composite {
    //The index will be modified based on the result of
    //the conditional.
    //0: True Branch
    //1: Conditional
    //2: False Branch
    //3: Get out of branch
    private int currentChildIndex = 1;

    //Current execution status
    private TaskStatus executionStatus = TaskStatus.Inactive;

    //Get the current index
    public override int CurrentChildIndex () {
        return currentChildIndex;
    }

    //Can the next child be executed?
    public override bool CanExecute () {
        bool continueExecution = false;

        //Check the result of the conditional branch
        if (currentChildIndex == 1) {
            switch (executionStatus) {
                case TaskStatus.Success:
                    //Go to the true statement
                    currentChildIndex = 0;
                    break;
                case TaskStatus.Failure:
                    //Go to the false statement
                    currentChildIndex = 2;
                    break;
            }

            //Continue the execution after the
            //conditional check
            continueExecution = true;
        } else {
            //If the true/false branch is still running
            switch (executionStatus) {
                case TaskStatus.Inactive:
                case TaskStatus.Running:
                    continueExecution = true;
                    break;
            }
        }

        return continueExecution;
    }

    //Check to do when a child is executed
    public override void OnChildExecuted (TaskStatus childStatus) {
        executionStatus = childStatus;
    }

    //Check if the conditions are matched
    public override void OnAwake () {
        if (children.Count != 3)
            UnityEngine.Debug.LogError("ERROR - Branching should always have 3 childs");
    }

    //When the composite finishes
    public override void OnEnd () {
        currentChildIndex = 1;
        executionStatus = TaskStatus.Inactive;
    }
}
