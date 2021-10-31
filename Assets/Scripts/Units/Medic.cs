using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : MonoBehaviour
{
    public float healingRadius;
    public float healAmount;
    public float healTime;
    float prevTime;
    private void Start()
    {
        prevTime = Time.time;
    }
    void Update()
    {
        float time = Time.time - prevTime;
        if (time >= healTime)
        {
            prevTime = Time.time;
            ApplyHeal();
        }
    }
    void ApplyHeal()
    {
        Collider2D[] allies = Physics2D.OverlapCircleAll(transform.position, healingRadius, LayerMask.GetMask("Allies"));
        foreach (Collider2D allie in allies)
        {
            Health health = allie.GetComponent<Health>();
            health.Heal(healAmount);
        }
    }
}
