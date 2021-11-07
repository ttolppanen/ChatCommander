using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip daySound;
    [SerializeField] AudioClip nightSound;
    [SerializeField] float volume;

    AudioSource daySoundSource;
    AudioSource nightSoundSource;

    private void Awake()
    {
        daySoundSource = gameObject.AddComponent<AudioSource>();
        daySoundSource.clip = daySound;
        daySoundSource.loop = true;
        nightSoundSource = gameObject.AddComponent<AudioSource>();
        nightSoundSource.clip = nightSound;
        nightSoundSource.loop = true;

        daySoundSource.Play();
        nightSoundSource.Play();
    }

    private void Update()
    {
        float timeValue = (1 + Mathf.Cos(DayCycle.timeOfDay)) / 2;
        daySoundSource.volume = volume * timeValue;
        nightSoundSource.volume = volume * (1 - timeValue);
    }
}
