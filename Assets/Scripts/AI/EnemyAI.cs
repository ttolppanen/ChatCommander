using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : AI
{
    public override void Awake()
    {
        base.Awake();
        enemiesReference = PlayerSpawner.Instance.GetAlliedUnits();
    }
    private void Start()
    {
        StartMoving(new Vector2(transform.position.x, -20));
    }

    public override void Update()
    {
        if (mood.DoIWantToMove())
        {
            StartMovingToANewPlace();
        }
        if (mood.DoIWantToStop())
        {
            StopMoving();
        }
        if (mood.DoIWantToShoot())
        {
            List<GameObject> visibleEnemies = VisibleEnemies();
            if (visibleEnemies.Count > 0)
            {
                Transform enemy = visibleEnemies[Random.Range(0, visibleEnemies.Count)].transform;
                StartAiming(enemy);
            }
        }
        base.Update();
    }
    void StartMovingToANewPlace()
    {
        Vector2 newPos = (Vector2)transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(0f, -3f));
        if (GM.ins.IsInsideMap(newPos))
        {
            StartMoving(newPos);
            return;
        }
        StartMovingToANewPlace();
    }
}
