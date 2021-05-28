using System.Collections;
using UnityEngine;

//Damage Testing: this is useful for testing the agents
public class DamageInducer : MonoBehaviour {
    //Damage to do on each simulated hit
    public int damageToDo = 10;

    //Time Between each Damage Hit
    public float timeBetweenDamages = 1f;

    //Wether the system should be active
    public bool isActive = true;

    //How many hits to do
    public int timesToPerform = -1;

    //Check if the required time has passed
    private bool timePassed = true;

    //Health system
    private HealthSystem health;

    //Get the health system from the object
    private void Awake () {
        health = GetComponent<HealthSystem>();
    }

    //Update
    private void Update () {
        if (isActive) {
            if (timePassed) {
                if (timesToPerform != 0) {
                    health.DamageAgent(damageToDo);
                    if (timesToPerform > 0)
                        timesToPerform--;
                    StartCoroutine(TimeToWait());
                }
            }
        }
    }

    //Wait for timer
    private IEnumerator TimeToWait () {
        timePassed = false;
        yield return new WaitForSeconds(timeBetweenDamages);
        timePassed = true;
    }
}