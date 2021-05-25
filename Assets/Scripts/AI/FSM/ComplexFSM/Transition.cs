namespace ComplexFSM {
    //Finite State Machine Transition
    public abstract class Transition {
        //Check if the transition can be done
        public abstract bool CanTransition (Machine fsm_owner);

        //When the transition is being performed (optional)
        public virtual void OnTransition (Machine fsm_owner) {
        }

        //State to go after the transition
        public int stateDestination;

        //Transition Constructor
        public static Transition CreateTransition<T> (int destination)
          where T : Transition, new() {
            return new T {
                stateDestination = destination
            };
        }
    }
}