using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Template Action")]
public class BT_Template : BT_StateAction {
    //Variables
    /*!: TONS OF VARS ... */

    //OnUpdate Call
    public override TaskStatus OnUpdate () {
        //First entry execution
        if (!CurrentStateSet()) {
            SetState();

            /* CODE TO RUN */
        }

        //Consecutive executions
        /* CODE TO RUN */

        //What to return
        return TaskStatus.Success;
    }
}