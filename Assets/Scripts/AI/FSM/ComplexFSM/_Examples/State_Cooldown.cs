using UnityEngine;

namespace ComplexFSM {
    namespace Examples {
        //Cooldown state, will wait for a second
        public class State_Cooldown : State {
            float timer;
            const float timeToWait = 1.0f;

            public override void OnStateEnter (Machine fsm_owner) {
                stateFinished = false;
                timer = timeToWait;
                Debug.Log("Waiting for cooldown...");
            }

            public override void OnStateExit (Machine fsm_owner) {
                Debug.Log("Cooldown Finished!");
            }

            //Wait for timer
            public override void OnStateUpdate (Machine fsm_owner) {
                if (timer <= 0f)
                    stateFinished = true;
                else
                    timer -= Time.deltaTime;
            }
        }
    }
}