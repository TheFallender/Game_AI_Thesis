using UnityEngine;

namespace ImplementationExamples {
    namespace FSM {
        public class BadExample : MonoBehaviour {
            bool initialized = false;

            private void Update () {
                //Check if the machine has been initialized
                if (!initialized) {
                    //Initialize Machine parameters
                    initialized = true;
                } else {
                    if (PlayerInView()) {
                        if (IsHealthLow()) {
                            //TODO: Flee
                        } else {
                            //TODO: Attack
                        }
                    } else if (DetectedSound()) {
                        //TODO: Investigate
                    } else {
                        //TODO: Patrol
                    }
                }
            }

            bool PlayerInView () {
                //TODO: Check if the player can be seen
                return false;
            }

            bool DetectedSound () {
                //TODO: Sound detection system
                return false;
            }

            bool IsHealthLow () {
                //TODO: Health below a defined threshold
                return false;
            }
        }
    }
}