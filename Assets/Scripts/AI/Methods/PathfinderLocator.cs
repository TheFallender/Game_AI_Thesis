using UnityEngine;

public class PathfinderLocator : MonoBehaviour {
    public static AstarPath pathfinder;

    private void Awake() {
        pathfinder = GetComponent<AstarPath>();
    }
}
