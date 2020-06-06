using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    float timePressed;
    public float maxPressedTime;
    public float minSpringScale;

    public float minSpringForce;
    public float maxSpringForce;

    public GameObject exitLockObject;

    GameObject parentObject;
    Rigidbody2D ballRigidBody;
    Ball ballScript;
    BoxCollider2D selfCollision;

    bool canPlayAudio = true;

    void Start()
    {
        parentObject = gameObject.transform.parent.gameObject;
        ballRigidBody = null;
        selfCollision = GetComponent<BoxCollider2D>();
        timePressed = 0f;
    }

    void Update()
    {
        if (Input.GetButton("Spring"))
        {
            if (canPlayAudio)
                StringSound();
            timePressed += Time.deltaTime;
            if (timePressed > maxPressedTime)
                timePressed = maxPressedTime;
            LerpSpringScale();
        }

        if (Input.GetButtonUp("Spring"))
        {
            canPlayAudio = true;
            AudioManager.Instance.PlaySound(AudioKey.SpringRelese);
            float pushForce = Mathf.Lerp(minSpringForce,maxSpringForce,timePressed / maxPressedTime);
            AddForceToBall(pushForce);
            timePressed = 0;
            StartCoroutine(ResetSpring());
        }
    }

    void LerpSpringScale()
    {        
        float newScale = Mathf.Lerp(1, minSpringScale, timePressed / maxPressedTime);
        parentObject.transform.localScale = new Vector3(1, newScale, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ballScript = collision.gameObject.GetComponent<Ball>();
        ballRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();

        exitLockObject.GetComponent<SpriteRenderer>().enabled = false;
        exitLockObject.GetComponent<PolygonCollider2D>().enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ballRigidBody = null;
    }

    void AddForceToBall(float pushForce)
    {
        if (ballScript != null)
            ballScript.IgnoreMaxVelocity();
        if (ballRigidBody != null)
            ballRigidBody.AddForce(new Vector2(0, 1.0f) * pushForce, ForceMode2D.Impulse);
    }

    IEnumerator ResetSpring()
    {
        selfCollision.enabled = false;
        LerpSpringScale();
        yield return new WaitForSeconds(.1f);
        selfCollision.enabled = true;
    }

    void StringSound()
    {
        canPlayAudio = false;
        AudioManager.Instance.PlaySound(AudioKey.SpringStretch);
    }
   
}