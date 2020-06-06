using System.Collections;
using UnityEngine;

public class deskBroken : MonoBehaviour
{
    public float ShakingDuration;
    public float SingleShakeLength;
    public float HorizontalShakeAmplitude;
    public float VerticalShakeAmplitude;

    public float HorizontalParticleAmplitude;
    public float VerticalParticleAmplitude;
    public int MinParticles;
    public int MaxParticles;

    public GameObject BarrelParticlePrefab;
    public GameObject WoodParticlePrefab;

    Vector3 OriginalPosition;
    public void Awake()
    {
        OriginalPosition = transform.position;
    }

    public void BreakOpen()
    {
        StartCoroutine(OpenSequence());
    }

    IEnumerator OpenSequence()
    {
        int stepCount = (int)(ShakingDuration / SingleShakeLength);
        for (int i = 0; i < stepCount; i++)
        {
            ShakePosition();
            yield return new WaitForSeconds(SingleShakeLength);
        }
        AudioManager.Instance.PlaySound(AudioKey.BossRoomPlank);
        SpawnParticles();
        Destroy(gameObject);
    }

    private void ShakePosition()
    {
        Vector3 offsetX = RandomVectorForce(new Vector3(HorizontalShakeAmplitude, 0, 0));
        Vector3 offsetY = RandomVectorForce(new Vector3(0, VerticalShakeAmplitude, 0));
        transform.position = OriginalPosition + offsetX + offsetY;
    }

    private Vector3 RandomVectorForce(Vector3 vector)
    {
        float force = Random.value;
        return vector * (2.0f * force - 1.0f);
    }

    private void SpawnParticles()
    {
        int particleCount = Random.Range(MinParticles, MaxParticles + 1);
        for(int i = 0; i< particleCount; i++)
        {
            SpawnRandomParticleInBounds();
        }
    }

    private void SpawnRandomParticleInBounds()
    {
        Vector3 offsetX = RandomVectorForce(new Vector3(HorizontalParticleAmplitude, 0, 0));
        Vector3 offsetY = RandomVectorForce(new Vector3(0, VerticalParticleAmplitude, 0));
        Vector2 spawnPosition = OriginalPosition + (offsetX + offsetY);
        if (Random.value < 0.5)
            ParticleSpawnManager.Instance.SpawnParticleForPrefab(WoodParticlePrefab, spawnPosition);
        else
            ParticleSpawnManager.Instance.SpawnParticleForPrefab(BarrelParticlePrefab, spawnPosition);
    }
}
