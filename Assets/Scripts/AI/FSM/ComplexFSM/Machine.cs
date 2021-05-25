using UnityEngine;
using System.Collections.Generic;

namespace ComplexFSM {
    //Finite State Machine
    public abstract class Machine : MonoBehaviour {
        //States available to the machine
        protected List<State> states;

        //Initial state (where it starts)
        protected int initialState;

        //Current State (the state the machine is currently on)
        protected State currentState;

        //Start and set the initial state
        protected void Awake () {
            currentState = states[initialState];
            currentState.OnStateEnter(this);
        }

        //Update
        protected void Update () {
            currentState.OnStateUpdate(this);
            if (currentState.stateFinished) {
                foreach (Transition trans in currentState.transitions) {
                    if (trans.CanTransition(this)) {
                        trans.OnTransition(this);
                        SetState(trans.stateDestination);
                    }
                }
            }
        }

        //Change the state and call the methods
        protected void SetState (int newState) {
            currentState.OnStateExit(this);
            currentState = states[newState];
            currentState.OnStateEnter(this);
        }
    }
}