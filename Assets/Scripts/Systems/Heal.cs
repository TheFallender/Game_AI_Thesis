using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Heal: will add it to the agent/player inventory
public class Heal : MonoBehaviour {
    //All heals list
    private static List<Heal> listOfHeals = new List<Heal>();

    //Type of heal
    private enum HealType {
        FullHeal,
        PartialHeal,
        SteppedHeal
    }

    //Parameters available
    [SerializeField] private HealType typeOfHeal;
    public int healthToRestore = 50;
    public bool instantlyConsume = false;
    public bool hasBeenTaken = false;

    //Tags to compare to the detected gameObject
    public List<string> tagsToCompare;

    //Add it to the main list
    private void Awake () {
        listOfHeals.Add(this);
    }

    //Use heal
    public void UseHeal (HealthSystem healthSystem) {
        switch (typeOfHeal) {
            case HealType.FullHeal:
                healthSystem.RestoreHealth();
                break;
            case HealType.PartialHeal:
                healthSystem.RestoreHealth(healthToRestore);
                break;
            case HealType.SteppedHeal:
                healthSystem.RestoreHealthStepped();
                break;
        }

        //Destroy the object holding the heal
        Destroy(gameObject);
    }

    //Finds Nearest heal
    public static Heal FindHealNearby (Transform pointOfReference) {
        //Variables to use on the for loop
        float lowestDistance = 0f;
        Heal nearestHeal = null;

        //Find the nearest one
        foreach (Heal healObject in listOfHeals) {
            float distanceToHeal = GoTo.DistanceToPoint(pointOfReference, healObject.transform);
            if (nearestHeal == null || distanceToHeal < lowestDistance) {
                nearestHeal = healObject;
                lowestDistance = distanceToHeal;
            }
        }

        return nearestHeal;
    }

    //When an object of the available types touches the heal
    private void OnTriggerEnter (Collider other) {
        if (TagChecks.CompareTags(other.gameObject, tagsToCompare)) {
            //Marked as taken
            hasBeenTaken = true;

            //Remove from the list
            listOfHeals.Remove(this);

            //Tell the agent that it has a heal
            other.GetComponent<RedirectToMainObject>().mainObject.GetComponent<HealthSystem>().OnHealCollected(this);

            //Disable future checks
            GetComponent<Collider>().enabled = false;

            //Disable rendering
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
