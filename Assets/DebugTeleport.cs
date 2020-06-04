using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleport : MonoBehaviour
{
    public GameObject BallReference;
    public Vector2 OutVelocity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("TELEPORT"))
        {
            BallReference.GetComponent<Rigidbody2D>().velocity = OutVelocity;
            BallReference.transform.position = this.transform.position;
            Debug.Log("DebugTeleport");
        }
    }
}
