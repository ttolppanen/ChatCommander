using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HitEffectDisplayer : MonoBehaviour
{
    [SerializeField] ParticleSystem effectPrefab;

    private IObjectPool<ParticleSystem> effectPool;

    private void Awake()
    {
        effectPool = new ObjectPool<ParticleSystem>(CreateEffect, OnGet, OnRelease);
    }

    private void OnGet(ParticleSystem effect)
    {
        effect.gameObject.SetActive(true);
        StartCoroutine(ReleaseEffect(effect));
    }
    private void OnRelease(ParticleSystem effect)
    {
        effect.gameObject.SetActive(false);
    }

    private ParticleSystem CreateEffect()
    {
        ParticleSystem effectInstance = Instantiate(effectPrefab);
        effectInstance.transform.parent = transform;
        return effectInstance;
    }
    public void PlayEffect(Vector2 point, float rotation)
    {
        ParticleSystem effect = effectPool.Get();
        effect.Play();
        effect.transform.position = point;
        effect.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    IEnumerator ReleaseEffect(ParticleSystem effect)
    {
        yield return new WaitForSeconds(3);
        effectPool.Release(effect);
    }
}
