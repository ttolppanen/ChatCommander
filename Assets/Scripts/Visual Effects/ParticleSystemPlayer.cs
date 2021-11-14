using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPlayer : MonoBehaviour, IVisualEffect
{
    public Transform GetTransform()
    {
        return transform;
    }

    public virtual void Play()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    public void SetActive(bool val)
    {
        gameObject.SetActive(val);
    }
}
