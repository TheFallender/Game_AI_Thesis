using UnityEngine;
using SGoap;

//GOAP Melee Attack
public class MeleeAttack : RangedAction {
    //Prevent agent from attacking constantly.
    public override float CooldownTime => 0.8f;

    //Action to execute when it's called
    public override EActionStatus Perform () {
        Debug.Log("Attack Player");
        return EActionStatus.Success;
    }

    public override bool PostPerform () {
        return true;
    }

    public override bool PrePerform () {
        return true;
    }
}