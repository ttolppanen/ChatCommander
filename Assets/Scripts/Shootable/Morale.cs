using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morale : MonoBehaviour, IDamageable, IShootable
{
    readonly float maxMorale = 1f;
    AI ai;
    Stats stats;
    float colRadius;

    float morale;

    private void Awake()
    {
        morale = maxMorale;
        ai = transform.parent.GetComponent<AI>();
        stats = transform.parent.GetComponent<Stats>();
        colRadius = GetComponent<CircleCollider2D>().radius;
    }

    public void Die()
    {
        ai.GetDown();
    }

    public float GetHpPercetage()
    {
        return morale / maxMorale;
    }

    public void Heal(float amount)
    {
        morale += amount;
        morale = Mathf.Clamp(morale + amount, 0, maxMorale);
        if (morale > 0.3f) ai.GetUp();
    }

    public bool OnHit(ShotInfo shot, Vector2 hitPosition)
    {
        Vector2 hitPointToCenter = (Vector2)transform.position - hitPosition;
        float hitMinDistance = (shot.dir * Vector2.Dot(shot.dir, hitPointToCenter) - hitPointToCenter).magnitude;
        float damage = hitMinDistance / colRadius;
        TakeDamage(damage);
        return false;
    }

    public void TakeDamage(float damage)
    {
        morale -= damage * (10f/(10f + stats.morale));
        if (morale <= 0)
        {
            morale = 0;
            Die();
        }
    }

    private void Update()
    {
        Heal(0.05f * stats.morale/10f * Time.deltaTime);
    }
}
