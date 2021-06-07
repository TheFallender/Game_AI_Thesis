using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("BT_Guard")]
public class BT_Guard : BT_Agent {
    //Systems
    [SerializeField] private Detection detection;
    [SerializeField] private Flee flee;

    [SerializeField] private Behavior tree;

    //States to update externally
    public SharedBool hasTarget;
    public SharedBool shouldFlee;
    public SharedBool hasHeal;
    public SharedInt lowHealth;
    public SharedAgentData agentData;

    //Awake method: Gets all the needed systems
    public override void OnAwake () {
        //DamageTaken and HealCollected will update the
        //machine from the outside to reduce the
        //performance hit it will use an event
        agentData.Value.health.DamageTaken += OnDamageTaken;
        agentData.Value.health.HealCollected += OnHealCollected;

        //Detection system
        detection.TargetDetected += OnTargetDetection;
    }

    //Method subscribed to when the agent gets damaged
    private void OnDamageTaken (int health, int maxHealth, float threshold) {
        //Check if there is a need for a state change
        if (health <= 0) {
            //Set that the agent is dead
            lowHealth.Value = 2;
        } else if (health < maxHealth * threshold) {
            //Set that the agent has to flee
            lowHealth.Value = 1;
            shouldFlee.Value = true;

            flee.ResetCounter();
        }
    }

    //Method subscribed to when the agent collects a heal
    private void OnHealCollected (Heal healToAdd) {
        //Delete previous heal if it has one
        if (agentData.Value.availableHeal != null)
            agentData.Value.availableHeal.DeleteHeal();

        //Overrides previous heal
        agentData.Value.availableHeal = healToAdd;

        //Set that the agent has a heal
        hasHeal.Value = true;
    }

    //Method subscribed to when the agent detects a valid target
    private void OnTargetDetection (Transform targetDetected) {
        if (targetDetected.GetComponent<HealthSystem>().IsAlive()) {
            //Set that the agent has a target
            hasTarget.Value = true;

            //Set the target
            agentData.Value.target = targetDetected;
        }
    }
}