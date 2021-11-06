using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : AI
{
    public override void Awake()
    {
        base.Awake();
        enemiesReference = EnemySpawner.Instance.enemiesOnField;
        gameObject.AddComponent<AlliedUnitInputReveiver>();
    }
    public override void Update()
    {
        base.Update();
        if (mood.DoIWantToShoot())
        {
            List<GameObject> visibleEnemies = VisibleEnemies();
            if (visibleEnemies.Count > 0)
            {
                Transform enemy = visibleEnemies[Random.Range(0, visibleEnemies.Count)].transform;
                StartAiming(enemy);
            }
        }
    }
}
public enum UnitTypes { soldier, engineer, medic }