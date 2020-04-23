using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
    public Transform startPos;
    public float delay;
    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(respaw(collision));
    }

    IEnumerator respaw(Collider2D ball)
    {
        yield return new WaitForSeconds(delay);
        ball.gameObject.transform.position = startPos.position;
    }
}
