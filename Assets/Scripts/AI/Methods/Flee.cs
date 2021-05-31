using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {
    //Transform to test with
    [SerializeField] Transform fleeDestination;

    //Max distance allowed from the agent
    [SerializeField] float sizeWanted;

    //Wait between flees
    public int timesToFlee;             //Times to flee
    private int fleeCounter;            //Times it has perfomed a flee
    public float waitBetweenFlees;      //Wait time between flees
    public bool fleeFinished = true;    //Has the previous flee finished
    private IEnumerator fleeWait;       //Enumerator to allow cancelling the wait

    //Get the next flee point
    public Transform GetNewFlee () {
        //Get a random point in a plane
        fleeDestination.position = GetPointInPlane();

        //If it hits the ground set that point, else, go to the zero coordinates
        if (!Waypoint.SetPointOnTheGround(fleeDestination, sizeWanted))
            fleeDestination.position = Vector3.zero;

        //Make a new flee wait
        MakeFleeWait();

        return fleeDestination;
    }

    //Get a random point inside a sphere
    private Vector3 GetPointInPlane () {
        Vector3 point = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));
        point *= sizeWanted;
        point += transform.position;
        return point;
    }

    //Make a new flee wait
    private void MakeFleeWait () {
        //Start a new flee wait
        fleeWait = FleeWait();
        StartCoroutine(fleeWait);

        //If it's not set to flee forever
        //decrease the flee counter
        if (fleeCounter > 0)
            fleeCounter--;
    }

    //Stop the flee wait to avoid waiting on a point
    public void StopFleeWait () {
        if (fleeWait != null)
            StopCoroutine(fleeWait);
        fleeFinished = true;
    }

    //Does it need to flee?
    public bool MustItFlee () {
        //If it's greater than 0 there are
        //flees left, if it's less than 0
        //it's set to flee forever
        return fleeCounter != 0 || !fleeFinished;
    }

    //Reset the counter of flees
    public void ResetCounter () {
        fleeCounter = timesToFlee;
    }

    //Enable full panic
    public void FleeForever () {
        fleeCounter = -1;
    }

    //Time to wait between flees/how long they should last
    private IEnumerator FleeWait () {
        fleeFinished = false;
        yield return new WaitForSeconds(waitBetweenFlees);
        fleeFinished = true;
    }
}
