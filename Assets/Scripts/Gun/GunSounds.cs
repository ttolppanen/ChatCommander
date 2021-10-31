using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSounds : MonoBehaviour
{
    public static float volume = 0.1f;
    public List<AudioClip> fireClips = new List<AudioClip>();
    List<int> fireSoundsToPlay = new List<int>();
    int previousFireClip;

    private void Awake()
    {
        for (int i = 0; i < fireClips.Count; i++)
        {
            fireSoundsToPlay.Add(i);
        }
    }
    public void PlayFireSound()
    {
        int clipToPlay = fireSoundsToPlay[Random.Range(0, fireSoundsToPlay.Count)];
        fireSoundsToPlay.Remove(clipToPlay);
        if (!fireSoundsToPlay.Contains(previousFireClip))
        {
            fireSoundsToPlay.Add(previousFireClip);
        }
        previousFireClip = clipToPlay;
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.volume = volume;
        audio.clip = fireClips[clipToPlay];
        audio.Play();
        Destroy(audio, 3f);
    }
}
