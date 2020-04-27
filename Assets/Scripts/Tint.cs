using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tint : MonoBehaviour
{
    Material tintMaterial;
    float tintTime = 0.3f;
    int TintStrength = 3;

    void Start()
    {
        tintMaterial = GetComponent<SpriteRenderer>().material;  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        tintMaterial.SetVector("_Vector4", new Vector4(TintStrength, TintStrength, TintStrength, 1));
        StartCoroutine(revertTint());
    }

    IEnumerator revertTint()
    {
        yield return new WaitForSeconds(tintTime);
        tintMaterial.SetVector("_Vector4", new Vector4(1, 1, 1, 1));
    }
}
