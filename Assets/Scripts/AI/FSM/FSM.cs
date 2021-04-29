using UnityEngine;
using System.Collections.Generic;

//Finite State Machine
public abstract class FSM : MonoBehaviour {
    //States available to the machine
    List<FSM_State> states;

    //Initial state (where it starts)
    FSM_State initialState;

    //Current State (the state the machine is currently on)
    FSM_State currentState;
}