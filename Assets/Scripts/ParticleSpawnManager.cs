﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleSpawnManager : MonoBehaviour
{
    public ParticleDictionaryEntry[] ParticleDictionary;
    public GameObject ParticleParentPrefab;

    public static ParticleSpawnManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnParticle(ParticleTag tag, Vector3 position)
    {
        ParticleDictionaryEntry[] entries = ParticleDictionary.Where(e => e.Tag == tag).ToArray();
        foreach(ParticleDictionaryEntry entry in entries) {
            GameObject particleParent = Instantiate(ParticleParentPrefab, position, Quaternion.identity);
            ParticleSystem particleSystem = Instantiate(entry.ParticlePrefab, particleParent.transform).GetComponent<ParticleSystem>();
            ParticleParent parentScript = particleParent.GetComponent<ParticleParent>();
            parentScript.Initiate(particleSystem);
        }
    }
}

[System.Serializable]
public struct ParticleDictionaryEntry
{
    public ParticleTag Tag;
    public GameObject ParticlePrefab;
}

[System.Serializable]
public enum ParticleTag
{
    Tag1,
    Tag2
}