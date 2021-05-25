using UnityEngine;

namespace ComplexFSM {
    namespace Examples {
        public class State_Attack : State {
            public override void OnStateEnter (Machine fsm_owner) {}

            public override void OnStateExit (Machine fsm_owner) {}

            public override void OnStateUpdate (Machine fsm_owner) {
                Debug.Log("Attacking Player!!!");
            }
        }
    }
}