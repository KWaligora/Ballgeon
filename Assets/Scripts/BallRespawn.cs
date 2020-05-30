using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawn : MonoBehaviour
{
    public Transform startPos;
    public float delay;
    private GameObject ballObject;
    void OnTriggerEnter2D(Collider2D collision)
    {
        ballObject = collision.gameObject;
        ScoreManager.Instance.OnLifeDown();
    }

    public void RespawnBall()
    {
        StartCoroutine(RespawnCoroutine(ballObject));
    }

    IEnumerator RespawnCoroutine(GameObject ball)
    {
        yield return new WaitForSeconds(delay);
        ball.gameObject.transform.position = startPos.position;
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }
}
