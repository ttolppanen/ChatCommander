using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] AudioClip soundEffect;
    [SerializeField] AudioSource effectPrefab;
    [SerializeField] float volume;

    private IObjectPool<AudioSource> effectPool;

    private void Awake()
    {
        effectPool = new ObjectPool<AudioSource>(CreateEffect, OnGet, OnRelease);
    }

    private void OnGet(AudioSource effect)
    {
        effect.gameObject.SetActive(true);
        StartCoroutine(ReleaseEffect(effect));
    }
    private void OnRelease(AudioSource effect)
    {
        effect.gameObject.SetActive(false);
    }

    private AudioSource CreateEffect()
    {
        AudioSource effectInstance = Instantiate(effectPrefab);
        effectInstance.transform.parent = transform;
        effectInstance.clip = soundEffect;
        effectInstance.volume = volume;
        return effectInstance;
    }
    public void PlayEffect(Vector2 point)
    {
        AudioSource effect = effectPool.Get();
        effect.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        effect.Play();
        effect.transform.position = point;
    }
    IEnumerator ReleaseEffect(AudioSource effect)
    {
        yield return new WaitForSeconds(1);
        effectPool.Release(effect);
    }
}
