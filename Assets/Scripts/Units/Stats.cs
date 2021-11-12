using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stats : MonoBehaviour
{
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
        GiveAccuracy(Random.Range(1f, 10f));
        GiveMovementSpeed(Random.Range(0f, 0.5f));
        GiveMorale(Random.Range(1f, 10f));
        GiveGunHandling(Random.Range(1f, 10f));
        GiveReloadSpeed(Random.Range(1f, 10f));
        UpdateReloadSpeed();
    }

    void LevelUp()
    {
        level++;
        GiveAccuracy(Random.Range(1f, 10f));
        GiveMovementSpeed(Random.Range(0f, 0.3f));
        GiveMorale(Random.Range(1f, 10f));
        GiveGunHandling(Random.Range(1f, 10f));
        GiveReloadSpeed(Random.Range(1f, 10f));
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
