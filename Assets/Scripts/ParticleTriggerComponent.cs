using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTriggerComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleTag tag;
        if (Enum.TryParse(gameObject.tag, out tag))
        {
            ParticleSpawnManager.Instance.SpawnParticle(tag, collision.transform.position);
        }
    }
}
