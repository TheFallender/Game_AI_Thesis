using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Dead Action")]
public class BT_Dead : BT_StateAction {
    //Variables
    [SerializeField] private GameObject mainObject;

    //OnUpdate Call
    public override TaskStatus OnUpdate () {
        if (agentData.Value.availableHeal)
            agentData.Value.availableHeal.DeleteHeal();

        Object.Destroy(mainObject);

        return TaskStatus.Success;
    }
}