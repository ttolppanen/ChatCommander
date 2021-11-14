using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Stats : MonoBehaviour
{
    public static event Action<Vector2, int> OnLevelUpAction;

    public float maxHp;
    public float movementSpeed;
    public float accuracy;
    public float morale;
    public float gunHandling;
    public float reloadSpeed;
    public float expOnDeath;
    public int level = 1;
    public float exp = 0;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GiveAccuracy(UnityEngine.Random.Range(1f, 10f));
        GiveMovementSpeed(UnityEngine.Random.Range(0f, 0.5f));
        GiveMorale(UnityEngine.Random.Range(1f, 10f));
        GiveGunHandling(UnityEngine.Random.Range(1f, 10f));
        GiveReloadSpeed(UnityEngine.Random.Range(1f, 10f));
        UpdateReloadSpeed();
    }

    void LevelUp()
    {
        level++;
        GiveAccuracy(UnityEngine.Random.Range(1f, 10f));
        GiveMovementSpeed(UnityEngine.Random.Range(0f, 0.3f));
        GiveMorale(UnityEngine.Random.Range(1f, 10f));
        GiveGunHandling(UnityEngine.Random.Range(1f, 10f));
        GiveReloadSpeed(UnityEngine.Random.Range(1f, 10f));
        OnLevelUpAction?.Invoke(transform.position, level);
        UpdateReloadSpeed();
    }

    public void GiveMovementSpeed(float amount)
    {
        movementSpeed += amount;
        agent.speed = movementSpeed;
    }
    public void GiveAccuracy(float amount)
    {
        accuracy += amount;
    }
    public void GiveMorale(float amount)
    {
        morale += amount;
    }
    public void GiveGunHandling(float amount)
    {
        gunHandling += amount;
    }
    public void GiveReloadSpeed(float amount)
    {
        reloadSpeed += amount;
    }
    public void GiveExp(float amount)
    {
        exp += amount;
        if (exp >= 10)
        {
            exp -= 10;
            LevelUp();
        }
    }
    void UpdateReloadSpeed()
    {
        GetComponent<Animator>().SetFloat("ReloadSpeed", reloadSpeed / 20);
    }
}
