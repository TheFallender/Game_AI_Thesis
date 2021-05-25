namespace ComplexFSM {
    namespace Examples {
        //Transition for testing purposes
        public class Transition_Always : Transition {
            public override bool CanTransition (Machine fsm_owner) {
                return true;
            }
        }
    }
}
