using System.Collections;
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

    public void SpawnParticle(ParticleTag tag, Vector2 position)
    {
        ParticleDictionaryEntry[] entries = ParticleDictionary.Where(e => e.Tag == tag).ToArray();
        foreach(ParticleDictionaryEntry entry in entries) {
            SpawnParticleForPrefab(entry.ParticlePrefab, position);
        }
    }

    public void SpawnParticleForPrefab(GameObject prefab, Vector2 position)
    {
        GameObject particleParent = Instantiate(ParticleParentPrefab, position, Quaternion.identity);
        ParticleSystem particleSystem = Instantiate(prefab, particleParent.transform).GetComponent<ParticleSystem>();
        ParticleParent parentScript = particleParent.GetComponent<ParticleParent>();
        parentScript.Initiate(particleSystem);
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
    gold,
    curtain,
    eye,
    grave,
    barrel,
    bone,
    spike,
    bat,
    shield,
    dragonspike,
    goblin,
    ghost,
    dragon,
    wood,
    wall
}
