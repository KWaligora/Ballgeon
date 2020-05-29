using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemys
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeDamage();
    }
}
