using UnityEngine;
using SGoap;

public class GOAP_Guard : MainAgent {
    //Systems
    private Detection detection;
    private Flee flee;

    //States to update externally
    [SerializeField] private StringReference hasTarget;
    [SerializeField] private StringReference shouldFlee;
    [SerializeField] private StringReference hasHeal;
    [SerializeField] private StringReference lowHealth;

    //Awake method: Gets all the needed systems
    private new void Awake () {
        //Call parent awake first
        base.Awake();

        //DamageTaken and HealCollected will update the
        //machine from the outside to reduce the
        //performance hit it will use an event
        AgentData.health = GetComponent<HealthSystem>();
        AgentData.health.DamageTaken += OnDamageTaken;
        AgentData.health.HealCollected += OnHealCollected;

        //GoTo system
        AgentData.goTo = GetComponentInChildren<GoTo>();

        //Detection system
        detection = GetComponentInChildren<Detection>();
        detection.TargetDetected += OnTargetDetection;

        //Flee system
        flee = GetComponentInChildren<Flee>();

        //Initialize states
        States.AddState(lowHealth.ToString(), 0f);
    }


    #region EventMethods

    //Method subscribed to when the agent gets damaged
    private void OnDamageTaken (int health, int maxHealth, float threshold) {
        //Check if there is a need for a state change
        if (health <= 0) {
            //Set that the agent is dead
            States.SetState(
                lowHealth.ToString(),
                2f
            );

            ForceReplan();
        } else if (health < maxHealth * threshold) {
            //Set that the agent has to flee
            States.SetState(
                shouldFlee.ToString(),
                1f
            );

            flee.ResetCounter();

            //Set that the agent has low health
            States.SetState(
                lowHealth.ToString(),
                1f
            );

            ForceReplan();
        }
    }

    //Method subscribed to when the agent collects a heal
    private void OnHealCollected (Heal healToAdd) {
        //Delete previous heal if it has one
        if (AgentData.AvailableHeal != null)
            AgentData.AvailableHeal.DeleteHeal();

        //Overrides previous heal
        AgentData.AvailableHeal = healToAdd;

        //Set that the agent has a heal
        States.SetState(
            hasHeal.ToString(),
            1f
        );
    }

    //Method subscribed to when the agent detects a valid target
    private void OnTargetDetection (Transform targetDetected) {
        if (targetDetected.GetComponent<HealthSystem>().IsAlive()) {
            //Set that the agent has a target
            States.SetState(
                hasTarget.ToString(),
                1f
            );

            //Set the target
            AgentData.Target = targetDetected;

            ForceReplan();
        }
    }

    #endregion EventMethods
}