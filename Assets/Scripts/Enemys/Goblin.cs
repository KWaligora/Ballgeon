using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemys
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            TakeDamage();
    }
}
