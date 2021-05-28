using UnityEngine;

//Health System for both the player and the agents
public class HealthSystem : MonoBehaviour {
    //Available Health
    public int maxHealth;
    public int currentHealth;

    //Health where a warning is issued
    [Range(0f, 1f)] public float healthThreshold;

    //Armor protection: reduces incomming damage in a percentage.
    [Range (0f, 1f)] public float armorValue;

    //Damage that each step does
    public int damagePerStep;

    //Damage Subscription
    public delegate void Del_DamageTaken (int health, int maxHealth, float threshold);
    public event Del_DamageTaken DamageTaken;

    //Heal Subscription
    public delegate void HealTouched (Heal healObject);
    public event HealTouched HealCollected;

    //Awake: Set the health to the max
    public void Awake () {
        RestoreHealth();
    }

    #region Damage

    //Method to damage the agent
    public void DamageAgent (int damageToDo) {
        float damageReduction = 1 - armorValue;
        currentHealth -= (int) (damageReduction * damageToDo);
        OnDamageTaken();
    }

    //Damage based on steps
    public void DamageAgentStepped () {
        currentHealth -= damagePerStep;
        OnDamageTaken();
    }

    #endregion Damage

    #region RestoreHealth

    //Restore Health Fully
    public void RestoreHealth () {
        currentHealth = maxHealth;
    }

    //Restore Health by an especified amount
    public void RestoreHealth (int healthToRestore) {
        currentHealth += healthToRestore;
        HealthCheckUps();
    }

    //Restore Health based the damageStep value
    public void RestoreHealthStepped () {
        currentHealth += damagePerStep;
        HealthCheckUps();
    }

    #endregion RestoreHealth

    #region ExtraMethods

    //Avoid having more health than the maximum allowed
    private void HealthCheckUps () {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    //Return the alive state of the owner
    public bool IsAlive () {
        return currentHealth > 0;
    }

    #endregion ExtraMethods

    #region Events

    //Event caller for when damage has been taken
    protected virtual void OnDamageTaken () {
        DamageTaken?.Invoke(currentHealth, maxHealth, healthThreshold);
    }

    //Event fo when a heal is collected
    public virtual void OnHealCollected (Heal heal) {
        HealCollected?.Invoke(heal);
    }

    #endregion Events

}
