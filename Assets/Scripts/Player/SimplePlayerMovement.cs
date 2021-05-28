using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour {
    //Direction to go
    private Vector3 dirToMove;

    //Rigidbody
    private Rigidbody rb;

    //Speed to move
    [SerializeField] private float speed;

    //Up, Down, Left, Right
    [SerializeField] private List<KeyCode> keysToMove = new List<KeyCode>(4);

    //Awake
    private void Awake () {
        rb = GetComponent<Rigidbody>();
    }

    //Update
    private void Update () {
        if (Input.GetKey(keysToMove[0]))
            dirToMove += Vector3.forward;
        if (Input.GetKey(keysToMove[1]))
            dirToMove += Vector3.back;
        if (Input.GetKey(keysToMove[2]))
            dirToMove += Vector3.left;
        if (Input.GetKey(keysToMove[3]))
            dirToMove += Vector3.right;
    }

    //FixedUpdate
    private void FixedUpdate () {
        if (dirToMove != Vector3.zero) {
            rb.velocity = new Vector3(0, rb.velocity.y, 0) + dirToMove * (speed * Time.fixedDeltaTime);
            dirToMove = Vector3.zero;
        } else {
            rb.velocity.Scale(new Vector3(0, 1, 0));
        }
    }
}
