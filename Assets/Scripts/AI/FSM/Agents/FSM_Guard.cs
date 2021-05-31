using System.Collections;
using UnityEngine;

//Guard FSM
public class FSM_Guard : MonoBehaviour {

    //Current State
    private State currentState;

    //Initial State
    [SerializeField] protected State initialState;

    //States available
    protected enum State {
        Init,       //Initial: Not used
        Patrol,     //Patrol: Go around the map checking for the player
        Attack,     //Attack: Attack until it's dead or the agent suffers great damage
        Flee,       //Flee: Panic around because of the low health
        GetHeals,   //GetHeals: Get the heals to recover from the damage
        Heal,       //Heal: Apply the heal
        Dead,       //Dead: Finish all the processing
        Recheck,    //Recheck: State that allows reentering the current state
    }

    //Systems
    [SerializeField] private HealthSystem health;
    [SerializeField] private GoTo goTo;
    [SerializeField] private Patrol patrol;
    [SerializeField] private Detection detection;
    [SerializeField] private Attack attack;
    [SerializeField] private Flee flee;

    //Variables of the Agent
    private State lastStateInitialized;             //For performing actions when entering a state

    //GoTo Variables
    [SerializeField] private float rangeOfAgent;    //Range available to the agent
    [SerializeField] private float rangeOfAttacks;  //Range of attacks of the agent

    //Health Variables
    private Heal availableHeal;                     //Heal that the agent has
    private Heal healWanted;                        //Heal that the agent is going for

    //Awake method: first time running (acts as initial state)
    private void Awake () {
        //DamageTaken and HealCollected will update the
        //machine from the outside to reduce the
        //performance hit it will use an event
        health.DamageTaken += OnDamageTaken;
        health.HealCollected += OnHealCollected;

        //Patrol system
        patrol.Initialize();

        //Detection system
        detection.TargetDetected += OnTargetDetection;

        //Set the initial state
        SetState(initialState);
    }

    //Update: will be called every frame
    private void Update () {
        switch (currentState) {
            //Patrol: going from one point to another
            case State.Patrol:
                //Check if there was a current waypoint setted
                if (!IsThisStateInit()) {
                    SetStateInit();

                    //Continue the waypoint
                    goTo.GoToPoint(patrol.GetCurrentWaypoint().transform, rangeOfAgent);
                } else {
                    //If the agent is near enough to the waypoint, get the next one
                    if (goTo.ReachedDestination()) {
                        //If it has already waited or there is no wait
                        if (!patrol.NeedToWait()) {
                            //Get the next waypoint info
                            goTo.GoToPoint(patrol.FindNextWaypoint().transform, rangeOfAgent);
                        } else {
                            //If it's not currently waiting
                            patrol.WaitingOnWaypoint();
                        }
                    }
                }
                break;
            //Attack the player
            case State.Attack:
                //Keep pointing at the target if it is close enough
                goTo.PointToObject();

                //The agent will continue to follow the target and
                //hit it until it's either dead or the agent has
                //taken enough damage to retreat or be dead
                if (!goTo.TargetExists() || !attack.TargetAlive()) {
                    attack.RemoveTarget();
                    SetState(State.Patrol);
                }
                break;
            //Flee to random points and panic
            case State.Flee:
                //If it must flee
                if (flee.MustItFlee()) {
                    //If it has reached the destination, disable the current wait
                    if (goTo.ReachedDestination())
                        flee.StopFleeWait();

                    //If it has finished fleeing get a new 
                    if (flee.fleeFinished)
                        goTo.GoToPoint(flee.GetNewFlee(), rangeOfAgent);
                } else {
                    //Flee state finished, go to the get heals
                    SetState(State.GetHeals);
                    //Reset the heals
                    flee.ResetCounter();
                }
                break;
            //Find all the heals available in the map
            case State.GetHeals:
                //If it's not going for a heal, go to it
                if (!IsThisStateInit() && availableHeal == null) {
                    SetStateInit();

                    //Find nearby heals
                    healWanted = Heal.FindHealNearby(transform);

                    //Agent in panic as there are no heals nearby
                    if (healWanted == null) {
                        SetState(State.Flee);
                        flee.FleeForever();
                    } else
                        goTo.GoToPoint(healWanted.transform, 0f);
                } else {
                    //If it has a heal, go to the healing state
                    if (availableHeal != null)
                        SetState(State.Heal);
                    else if (!goTo.TargetExists() || healWanted.hasBeenTaken) {
                        //If the heal has been taken or it has been consumed
                        //recheck for more heals
                        lastStateInitialized = State.Recheck;
                    }
                }
                break;
            //Heal based on the type of heal
            case State.Heal:
                //Use the heal
                availableHeal.UseHeal(health);

                //Delete the heal variables
                healWanted = null;
                availableHeal = null;

                //Go back to patrol
                SetState(State.Patrol);
                break;
            //Stop all processing and destroy agent gameobject
            case State.Dead:
                goTo.StopPathfinding();
                Destroy(transform.parent.parent.gameObject);
                break;
            //Edge case
            default:
                SetState(State.Dead);
                break;
        }
    }

    #region StateChanges
    //Change State: Centralized on a single function for better debugging
    private void SetState (State newState) {
        //Only change the state if the new state is different
        if (newState != currentState) {
            Debug.Log(string.Format("StateChanged from {0} to {1}", currentState, newState));
            currentState = newState;
        }
    }

    //Check if the current state has been initialized
    private bool IsThisStateInit () {
        return lastStateInitialized == currentState;
    }

    //Set the current state as the one initialized
    private void SetStateInit () {
        lastStateInitialized = currentState;
    }

    #endregion StateChanges

    #region EventMethods

    //Method subscribed to when the agent gets damaged
    private void OnDamageTaken (int health, int maxHealth, float threshold) {
        //Check if there is a need for a state change
        if (health <= 0) {
            SetState(State.Dead);
            attack.RemoveTarget();
        } else if (health < maxHealth * threshold) {
            SetState(State.Flee);
            attack.RemoveTarget();
            flee.ResetCounter();
        }
    }

    //Method subscribed to when the agent collects a heal
    private void OnHealCollected (Heal healToAdd) {
        //Overrides previous heal
        availableHeal = healToAdd;
    }

    //Method subscribed to when the agent detects a valid target
    private void OnTargetDetection (Transform targetDetected) {
        if (currentState == State.Patrol) {
            if (targetDetected.GetComponent<HealthSystem>().IsAlive()) {
                //Change to the attack state
                SetState(State.Attack);

                //Set the target
                attack.AssignTarget(targetDetected);

                //Set the target for the goto
                goTo.GoToPoint(targetDetected, rangeOfAttacks);
            }
        }
    }

    #endregion EventMethods
}