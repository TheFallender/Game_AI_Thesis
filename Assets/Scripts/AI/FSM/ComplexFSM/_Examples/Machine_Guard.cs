using System.Collections.Generic;

namespace ComplexFSM {
    namespace Examples {
        //Guard basic example
        public class Machine_Guard : Machine {
            private new void Awake () {
                states = new List<State>() {
                    //Attack State
                    State.CreateState<State_Attack>(
                        new List<Transition>() {
                            Transition.CreateTransition<Transition_Always>(1)
                        }
                    ),
                    //Cooldown State
                    State.CreateState<State_Cooldown>(
                        new List<Transition>() {
                            Transition.CreateTransition<Transition_Always>(0)
                        }
                    ),
                };

                initialState = 0;

                base.Awake();
            }
        }
    }
}