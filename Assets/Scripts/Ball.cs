using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{    
    public float maxVelocity;

    Rigidbody2D myRB;
    Vector2 currentVelocity;
    Vector2 maxVelocityVector;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        maxVelocityVector = new Vector2(maxVelocity, maxVelocity);
    }

    private void FixedUpdate()
    {
        currentVelocity = myRB.velocity;
        if (currentVelocity.magnitude > maxVelocityVector.magnitude) //if current velocity > max velocity use max velocity
        {
            myRB.velocity = currentVelocity.normalized * maxVelocity;
        }        
    }
}
