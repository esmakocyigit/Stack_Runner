using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField]
    AudioClip clickSound;
    [SerializeField]
    float initialPitch = 0.5f;
    [SerializeField]
    float pitchIncreaseAmount = 0.1f;

    AudioSource audioSource;
    float pitch;

    public override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        pitch = initialPitch;
    }

    public void PlayClickSound(bool isPerfect)
    {
        if (isPerfect)
        {
            pitch += pitchIncreaseAmount;
            PlayClickSound();
            return;
        }

        pitch = initialPitch;
    }

    void PlayClickSound()
    {
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clickSound);
    }
}
