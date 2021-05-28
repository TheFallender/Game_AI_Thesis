using System.Collections.Generic;
using UnityEngine;

//Tag methods
public class TagChecks {
    //Compare the available tags
    public static bool CompareTags (GameObject gameObjectToCheck, List<string> tags) {
        foreach (string tag in tags) {
            if (gameObjectToCheck.CompareTag(tag))
                return true;
        }
        return false;
    }
}