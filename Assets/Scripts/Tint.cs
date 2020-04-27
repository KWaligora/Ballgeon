using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tint : MonoBehaviour
{
    Material tintMaterial;

    void Start()
    {
        tintMaterial = GetComponent<SpriteRenderer>().material;  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        tintMaterial.SetVector("_Vector4", new Vector4(2,2,2,1));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        tintMaterial.SetVector("_Vector4", new Vector4(1, 1, 1, 1));
    }

}
