using UnityEngine;

namespace ImplementationExamples {
    namespace FSM {
        public class Basic_FSM : MonoBehaviour {
            private FSM_States currentState = FSM_States.S0_Initialize;

            private void Update () {
                switch (currentState) {
                    case FSM_States.S0_Initialize:
                        //Initialize Machine parameters
                        currentState = FSM_States.S1_Patrol;
                        break;
                    case FSM_States.S1_Patrol:
                        //Start the patrol
                        //TODO: Conditions for transitions
                        break;
                    case FSM_States.S2_Investigate:
                        //Investigate the noise
                        //TODO: Conditions for transitions
                        break;
                    case FSM_States.S3_Attack:
                        //Attack the enemy
                        //TODO: Conditions for transitions
                        break;
                    case FSM_States.S4_Flee:
                        //Flee to start position
                        //TODO: Conditions for transitions
                        break;
                    case FSM_States.S5_Dead:
                        //Dead: stop all process and disable AI
                        break;
                    default:
                        //Edge case
                        currentState = FSM_States.S1_Patrol;
                        break;
                }
            }
            protected enum FSM_States {
                S0_Initialize,
                S1_Patrol,
                S2_Investigate,
                S3_Attack,
                S4_Flee,
                S5_Dead
            }
        }
    }
}