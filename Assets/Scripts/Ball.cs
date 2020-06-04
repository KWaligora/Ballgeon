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

    public ParticleDictionaryEntry[] ParticleDictionary;

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

[System.Serializable]
public struct ParticleDictionaryEntry
{
    public ParticleTag Tag;
    public GameObject ParticlePrefab;
}

[System.Serializable]
public enum ParticleTag
{
    Tag1,
    Tag2
}
