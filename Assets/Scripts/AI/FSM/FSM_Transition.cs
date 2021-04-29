//Finite State Machine Transition
public abstract class FSM_Transition {
    //Check if the transition can be done
    public abstract bool CanTransition ();

    //When the transition is being performed (optional)
    public virtual void OnTransition () { }

    //State to go after the transition
    public FSM_State stateDestination;
}