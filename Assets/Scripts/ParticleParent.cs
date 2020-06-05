using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleParent : MonoBehaviour
{
    internal void Initiate(ParticleSystem particleSystem)
    {
        StartCoroutine(PlayAndKill(particleSystem));
    }

    IEnumerator PlayAndKill(ParticleSystem particleSystem)
    {
        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.main.duration + 0.5f);
        particleSystem.Stop();
        Destroy(gameObject);
    }
}
