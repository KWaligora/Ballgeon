using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Transform newPos;
    public int ScoreForTeleport;

    static bool teleporting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !teleporting)
        {
            StartCoroutine(Teleport(collision.gameObject));
            StartCoroutine(TeleportDelay());
        }                
    }

    IEnumerator Teleport(GameObject ball)
    {       
        Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();

        //Vector2 velocity = ballRB.velocity;
        ballRB.velocity = new Vector2(0, 0);
        ballRB.gravityScale = 0;

        yield return new WaitForSeconds(1);
        ball.transform.position = newPos.position;
        ballRB.gravityScale = 1;
        ballRB.velocity = new Vector2(-10, -10);
        ScoreManager.Instance.AddScore(ScoreForTeleport);
    }

    IEnumerator TeleportDelay()
    {
        teleporting = true;
        yield return new WaitForSeconds(2);
        teleporting = false;
    }
}
