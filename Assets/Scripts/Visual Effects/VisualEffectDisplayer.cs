using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class VisualEffectDisplayer : MonoBehaviour
{
    [SerializeField] GameObject effectPrefab;

    private IObjectPool<IVisualEffect> effectPool;

    private void Awake()
    {
        effectPool = new ObjectPool<IVisualEffect>(CreateEffect, OnGet, OnRelease);
    }

    private void OnGet(IVisualEffect effect)
    {
        effect.SetActive(true);
        StartCoroutine(ReleaseEffect(effect));
    }
    private void OnRelease(IVisualEffect effect)
    {
        effect.SetActive(false);
    }

    private IVisualEffect CreateEffect()
    {
        GameObject effectInstance = Instantiate(effectPrefab);
        effectInstance.transform.parent = transform;
        return effectInstance.GetComponent<IVisualEffect>();
    }
    public void PlayEffect(Vector2 point, float rotation)
    {
        IVisualEffect effect = effectPool.Get();
        effect.Play();
        effect.GetTransform().position = point;
        effect.GetTransform().rotation = Quaternion.Euler(0, 0, rotation);
    }
    IEnumerator ReleaseEffect(IVisualEffect effect)
    {
        yield return new WaitForSeconds(3);
        effectPool.Release(effect);
    }
}
