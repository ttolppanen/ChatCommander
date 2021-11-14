using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayEffectWithLight : ParticleSystemPlayer
{
    [SerializeField] float lightDisappearTime;

    Light2D lightComponent;

    float maxIntensity;
    float startTime;

    private void Awake()
    {
        lightComponent = GetComponentInChildren<Light2D>();
        maxIntensity = lightComponent.intensity;
    }

    public override void Play()
    {
        lightComponent.gameObject.SetActive(true);
        lightComponent.intensity = maxIntensity;
        startTime = Time.time;
    }

    private void Update()
    {
        lightComponent.intensity -= Time.deltaTime * maxIntensity / lightDisappearTime;
        if (lightComponent.intensity <= 0)
        {
            lightComponent.intensity = 0;
            lightComponent.gameObject.SetActive(false);
        }
    }
}
