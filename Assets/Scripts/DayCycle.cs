using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayCycle : MonoBehaviour
{
    public static float timeOfDay { get; private set; }

    public float[] intensity = new float[2];
    public float[] sunIntensity = new float[2];
    public float[] shadowIntensity = new float[2];
    public float startingTime;
    public float speed;
    public Vector3 sunRisePos;
    public Vector3 sunSetPos;
    public Color nightColor;
    public Color dayColor;
    public Color inBetweenColor;
    new Light2D light;
    Light2D sun;
    Transform sunTransform;

    private void Start()
    {
        light = GetComponent<Light2D>();
        sunTransform = transform.GetChild(0);
        sun = sunTransform.GetComponent<Light2D>();
    }

    void Update()
    {
        timeOfDay = startingTime + Time.time * speed;
        float c = (1 + Mathf.Cos(timeOfDay)) / 2;
        c = Mathf.Sqrt(c);
        float s = (1 + Mathf.Sin(timeOfDay)) / 2;
        Color col = Color.white;
        if (c < 0.5f)
        {
            col = Color.Lerp(nightColor, inBetweenColor, c / 0.5f);
        }
        else
        {
            col = Color.Lerp(inBetweenColor, dayColor, 2 * c - 1f);
        }
        sunTransform.position = Vector3.Lerp(sunRisePos, sunSetPos, s);
        light.color = col;
        sun.color = col;
        light.intensity = intensity[0] + (intensity[1] - intensity[0]) * c;
        sun.intensity = sunIntensity[0] + (sunIntensity[1] - sunIntensity[0]) * c;
        sun.intensity = Mathf.Pow(sun.intensity, 2);
        sun.shadowIntensity = shadowIntensity[0] + (shadowIntensity[1] - shadowIntensity[0]) * c;
    }
}
