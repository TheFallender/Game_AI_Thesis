using UnityEngine;

public class TrackObject : MonoBehaviour {
    private Transform cameraToMove;
    private Vector3 originalPos;

    public Transform objectToTrack;
    public bool keepXAxis = false;
    public bool keepYAxis = false;
    public bool keepZAxis = false;

    private bool trackObject = false;


    private void Awake () {
        cameraToMove = transform;
        originalPos = cameraToMove.position;
    }

    private void Update () {
        //Toggle Tracking
        if (Input.GetKeyDown(KeyCode.C)) {
            trackObject = !trackObject;
            if (!trackObject)
                cameraToMove.position = originalPos;
        }

        //Zoom System
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.KeypadMinus)) {
            float changeY = Input.GetKeyDown(KeyCode.KeypadPlus) ? -10f : 10f;
            cameraToMove.position += new Vector3(0, changeY);
        }

        //Perform Change
        if (trackObject) {
            cameraToMove.position = new Vector3() {
                x = keepXAxis ? cameraToMove.position.x : objectToTrack.position.x,
                y = keepYAxis ? cameraToMove.position.y : objectToTrack.position.y,
                z = keepZAxis ? cameraToMove.position.z : objectToTrack.position.z,
            };
        }
    }
}