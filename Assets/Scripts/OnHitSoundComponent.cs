using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitSoundComponent : MonoBehaviour
{
    public AudioKey SoundKey;
    public bool IsTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTrigger)
            PlayAudio();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsTrigger)
            PlayAudio();
    }

    private void PlayAudio()
    {
        AudioManager.Instance.PlaySound(SoundKey);
    }
}