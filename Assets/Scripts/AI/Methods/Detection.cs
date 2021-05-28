using UnityEngine;
using System.Collections.Generic;

//This system will detect a target with a specified tag
public class Detection : MonoBehaviour {
    //Tags to compare to the detected gameObject
    public List<string> tagsToCompare;

    //Detection Subscription
    public delegate void TargetFound (Transform targetDetected);
    public event TargetFound TargetDetected;

    //When a gameObject enters to the trigger zone
    private void OnTriggerStay (Collider other) {
        if (TagChecks.CompareTags(other.gameObject, tagsToCompare))
            OnTargetFound(other.transform);
    }

    //Invoke when a target has been found
    public virtual void OnTargetFound (Transform targetDetected) {
        TargetDetected?.Invoke(targetDetected);
    }
}