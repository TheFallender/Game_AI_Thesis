using System.Collections.Generic;

//Finite State Machine State
public abstract class FSM_State {
    //When it enters to the state
    public abstract void OnStateEnter ();

    //When the state gets updated
    public abstract void OnStateUpdate ();

    //When the state is going to do a transition
    public abstract void OnStateExit ();

    //Transitions available to the state
    List<FSM_Transition> transitions;
}