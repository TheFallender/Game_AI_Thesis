using System.Collections.Generic;
using System.Linq;

//Ranged Actions Template
public abstract class RangedAction : SGoap.MainAction {
    //Range needed for the action
    public float rangeNeeded;
    public UnityEngine.Transform target;
    public SGoap.StringReference stateToUpdate;

    //Get next ranged action in the Queue
    public static RangedAction GetNextRangedAction (Queue<SGoap.Action> currentPlan) {
        //Find the first one
        return (RangedAction) currentPlan.ToList().Find(act => act is RangedAction);
    }
}