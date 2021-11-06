using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject blood;
    public GameObject bloodSplatter;
    Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
    }
    public float TakeDamage(float damage)
    {
        stats.hp -= damage;
        if (stats.hp <= 0)
        {
            float exp = stats.expOnDeath;
            Die();
            return exp;
        }
        return 0.0f;
    }
    public void Heal(float amount)
    {
        stats.hp += amount;
        if (stats.hp >= stats.maxHp)
        {
            stats.hp = stats.maxHp;
        }
    }
    public void Die()
    {
        // YÖÖÖKK
        PlayerSpawner.Instance.GetAlliedUnits().Remove(gameObject);
        EnemySpawner.Instance.enemiesOnField.Remove(gameObject);
        Destroy(gameObject);
    }
    public void OnHitEffects(Vector2 hitPoint, Vector2 dir, Vector2 collPoint)
    {
        Vector2 centerToHit = hitPoint - collPoint;
        Vector2 bloodPoint = hitPoint - 2 * Vector2.Dot(dir, centerToHit) * dir;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject bloodSplatterInst = Instantiate(bloodSplatter, new Vector3(bloodPoint.x, bloodPoint.y, -5), Quaternion.Euler(0, 0, angle));
        Destroy(bloodSplatterInst, 5f);
        Instantiate(blood, transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
    }
}
