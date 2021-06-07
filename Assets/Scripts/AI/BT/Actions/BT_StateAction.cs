using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("StateAction")]
[TaskCategory("CustomActions")]
[TaskIcon("{SkinColor}ReflectionIcon.png")]
public abstract class BT_StateAction : Action {
    [Header("Agent Info")]
    //Shared variable for the current agent status
    [SerializeField] protected SharedAgentData agentData;
    [SerializeField] protected int stateID;

    //The agent state will have the following logic:
    //-1: Initial
    //0: Patrol
    //1: Attack
    //2: Flee
    //3: GetHeals
    //4: Heal
    //5: Dead
    //6: Recheck

    //Set the current state
    protected void SetState () {
        agentData.Value.agentState = stateID;
    }

    //Check if the current state matches the stateID
    protected bool CurrentStateSet () {
        return agentData.Value.agentState == stateID;
    }
}
