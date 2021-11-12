using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IShootable, IDamageable
{
    Stats stats;
    float hp;
    public static event Action<Vector2, float> OnBloodSplatterAction;

    private void Awake()
    {
        stats = GetComponent<Stats>();
        hp = stats.maxHp;
    }

    public bool OnHit(ShotInfo shot, Vector2 hitPosition)
    {
        if (UF.InPit(transform.position) && UF.Chance(0.5f)) return false;

        InvokeBloodSplatter(hitPosition, shot.dir, transform.position);
        TakeDamage(shot.damage);
        return true;
    }

    public void TakeDamage(float damage)
    {
        //Get Armor
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        hp += amount;
        if (hp >= stats.maxHp)
        {
            hp = stats.maxHp;
        }
    }
    public void Die()
    {
        // Y÷÷÷KK
        // Event I died? Give exp
        PlayerSpawner.Instance.GetAlliedUnits().Remove(gameObject);
        EnemySpawner.Instance.enemiesOnField.Remove(gameObject);
        Destroy(gameObject);
    }

    void InvokeBloodSplatter(Vector2 hitPoint, Vector2 dir, Vector2 collPoint) //collPoint = targetPosition
    {
        Vector2 centerToHit = hitPoint - collPoint;
        Vector2 bloodPoint = hitPoint - 2 * Vector2.Dot(dir, centerToHit) * dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        OnBloodSplatterAction?.Invoke(new Vector3(bloodPoint.x, bloodPoint.y, -5), angle);
    }
}
