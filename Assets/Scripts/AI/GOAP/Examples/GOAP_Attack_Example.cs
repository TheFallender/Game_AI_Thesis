using UnityEngine;
using SGoap;

//GOAP attack example
public class GOAP_Attack_Example : BasicAction {
    //Prevent agent from attacking constantly.
    public override float CooldownTime => 0.8f;

    //Action to execute when it's called
    public override EActionStatus Perform () {
        Debug.Log("Attack Player");
        return EActionStatus.Success;
    }
}