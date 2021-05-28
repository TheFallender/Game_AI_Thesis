using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //Variables needed
    [SerializeField] private int damageToDo;            //Damage to do on each attack
    [SerializeField] private AttackType typeOfAttack;   //Type of attack
    [SerializeField] private float cooldownTime = 0f;   //Wait Between attacks
    private bool cooldownActive = false;                //Whether a cooldown is active or not
    private Transform target;                           //Target it wants to attack
    public HealthSystem targetHealth;                   //Target health system

    //Type of attack
    private enum AttackType {
        NormalAttack,
        SteppedAttack
    }

    //Tags to compare to the detected gameObject
    public List<string> tagsToCompare;

    //Assign target and it's health system
    public void AssignTarget (Transform transformToFollow) {
        target = transformToFollow;
        targetHealth = transformToFollow.GetComponent<HealthSystem>();
        cooldownActive = false;
    }

    //Remove target
    public void RemoveTarget () {
        target = null;
        targetHealth = null;
        cooldownActive = true;
    }

    //Return if the target is alive
    public bool TargetAlive () {
        return targetHealth.IsAlive();
    }

    //Cooldown wait
    private IEnumerator Cooldown () {
        cooldownActive = true;
        yield return new WaitForSeconds(cooldownTime);
        cooldownActive = false;
    }

    //When the hitbox detects something
    private void OnTriggerStay (Collider other) {
        if (!cooldownActive)
            if (target != null)
                if (other.gameObject == target.gameObject) {
                    switch (typeOfAttack) {
                        case AttackType.NormalAttack:
                            targetHealth.DamageAgent(damageToDo);
                            break;
                        case AttackType.SteppedAttack:
                            targetHealth.DamageAgentStepped();
                            break;
                    }
                    StartCoroutine(Cooldown());
                }
    }
}
