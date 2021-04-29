using UnityEngine;

public class AIDestinationMove : MonoBehaviour {
    public Camera mainCam;
    private bool trackMouse = false;
    public Transform objectToMove;

    private void Awake () {
        if (!mainCam)
            mainCam = Camera.main;
    }

    private void Update () {
        //Toggle Tracking
        if (Input.GetKeyDown(KeyCode.T))
            trackMouse = !trackMouse;

        //Perform Change
        if (trackMouse) {
            if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
                objectToMove.position = hit.point;
            } 
        }
    }
}