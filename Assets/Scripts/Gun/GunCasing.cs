using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCasing : MonoBehaviour
{
    public float flyTime;
    float startTime;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        Destroy(gameObject, 60f);
    }

    private void Update()
    {
        float t = Time.time - startTime;
        if (t <= flyTime)
        {
            Vector3 scale = transform.localScale;
            scale.x = UF.SecondDegree(t, 2, 1, flyTime, 1);
            scale.y = scale.x;
            transform.localScale = scale;
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }
}
