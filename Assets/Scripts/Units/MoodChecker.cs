using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodChecker : MonoBehaviour
{
    private Stats stats;

    private void Awake()
    {
        stats = GetComponent<Stats>();
    }
    public bool AdjustGun()
    {
        return UF.Chance(2 * 10f / stats.gunHandling * Time.deltaTime);
    }
    public bool IWantToPullTrigger()
    {
        return UF.Chance((stats.morale + stats.accuracy) / 200f * Time.deltaTime);
    }
    public bool IWantToStopAiming()
    {
        return UF.Chance(10f / (stats.morale + 100f) * Time.deltaTime);
    }
    public int HowManyBulletsIWantToShoot()
    {
        return Random.Range(1, 2 + (int)(stats.morale / 5));
    }
    public bool DoIWantToShoot()
    {
        return UF.Chance(0.1f * Time.deltaTime * stats.morale);
    }
    public float AimingError()
    {
        return Random.Range(-20f, 20f) * 20 / (1.5f * (stats.accuracy + stats.gunHandling));
    }
    public bool DoIWantToStop()
    {
        return UF.Chance((stats.morale + 20f) / (100f + 20f) * Time.deltaTime);
    }
    public bool DoIWantToMove()
    {
        return UF.Chance((stats.morale) / (100f) * Time.deltaTime);
    }
}
