using System.Collections.Generic;
using System.Linq;

//Ranged Actions Template
public class RangedAction : SGoap.BasicAction {
    //Range needed for the action
    public float RangeNeeded;

    //Get the Range of the next ranged action in the Queue
    public static float GetRangeOfNextAction (Queue<SGoap.Action> currentPlan) {
        //Get the Ranged action
        SGoap.Action firstRangedAction = currentPlan.ToList().Find(act => act is RangedAction);

        //Check if a ranged action was found
        if (firstRangedAction != null) {
            //Range value
            return ((RangedAction) firstRangedAction).RangeNeeded;
        } else
            //Error value
            return -1f;
    }
}