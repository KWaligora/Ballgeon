using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    public bool IsTrigger;
    public int ScoreAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTrigger)
            GrantScore();
    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsTrigger)
            GrantScore();
    }

    private void GrantScore()
    {
        ScoreManager.Instance.AddScore(ScoreAmount);
    }
}
