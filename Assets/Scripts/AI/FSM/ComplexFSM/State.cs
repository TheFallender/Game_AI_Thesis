using System.Collections.Generic;

namespace ComplexFSM {
    //Finite State Machine State
    public abstract class State {
        //When it enters to the state
        public abstract void OnStateEnter (Machine fsm_owner);

        //When the state gets updated
        public abstract void OnStateUpdate (Machine fsm_owner);

        //When the state is going to do a transition
        public abstract void OnStateExit (Machine fsm_owner);

        //Is State Finished
        public bool stateFinished = true;

        //Transitions available to the state
        public List<Transition> transitions;

        //State Constructor
        public static State CreateState<T> (List<Transition> availableTrans)
          where T : State, new() {
            return new T {
                transitions = availableTrans
            };
        }
    }
}