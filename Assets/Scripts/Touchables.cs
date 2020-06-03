using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchables : MonoBehaviour
{
    private Material material;
    private bool tinted = false;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            SetTint();
    }

    private void SetTint()
    {
        if (tinted)
        {
            tinted = false;
            material.SetVector("_Vector4", new Vector4(1, 1, 1, 1));
        }
        else
        {
            tinted = true;
            material.SetVector("_Vector4", new Vector4(2, 2, 2, 1));
        }
    }
}
