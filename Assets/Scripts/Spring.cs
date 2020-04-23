using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float delay;

    void OnTriggerEnter2D(Collider2D collision)
    {
       Rigidbody2D ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(AddForce(ballRB));
    }

    IEnumerator AddForce(Rigidbody2D ballRb)
    {
        float pushForce = Random.Range(20, 60);
        yield return new WaitForSeconds(delay);
        ballRb.AddForce(new Vector2(0, 1.0f) * pushForce, ForceMode2D.Impulse);
    }
}
