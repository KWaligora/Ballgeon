using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{    
    public float maxVelocity;

    Rigidbody2D myRB;
    Vector2 currentVelocity;
    Vector2 maxVelocityVector;
    bool ignoringMaxVelocity = false;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        maxVelocityVector = new Vector2(maxVelocity, maxVelocity);
    }
    
    //Disable velocity limit and enable it again later
    public void IgnoreMaxVelocity()
    {
        ignoringMaxVelocity = true;
        StartCoroutine(ApplyVelocityLimit());
    }

    //enable velocity limit after 2 seconds
    IEnumerator ApplyVelocityLimit()
    {
        yield return new WaitForSeconds(2.0f);
        ignoringMaxVelocity = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ParticleTag tag;
        if (Enum.TryParse(collision.gameObject.tag, out tag))
        {
            ParticleSpawnManager.Instance.SpawnParticle(tag, collision.GetContact(0).point);
        }
    }

    private void FixedUpdate()
    {
        if (!ignoringMaxVelocity)
        {
            currentVelocity = myRB.velocity;
            if (currentVelocity.magnitude > maxVelocityVector.magnitude) //if current velocity > max velocity use max velocity
            {
                myRB.velocity = currentVelocity.normalized * maxVelocity;
            }
        }
    }
}
