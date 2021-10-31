using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : AI
{
    public float startMovingBias;
    public float stopMovingBias;
    public override void Awake()
    {
        base.Awake();
        enemies = GM.ins.allies;
    }
    private void Start()
    {
        SetMovePoint(new Vector2(transform.position.x, -20));
    }

    public override void Update()
    {
        if (anim.GetAnimatorTransitionInfo(0).duration > 0 || InForbbiddenState())
        {
            return;
        }
        if (state == UnitStates.idle && DoIWantToMove())
        {
            StartMoving();
        }
        else if (state == UnitStates.moving && DoIWantToStop())
        {
            StopMovement();
        }
        else if ((state == UnitStates.idle || state == UnitStates.moving) && DoIWantToShoot())
        {
            List<GameObject> visibleEnemies = VisibleEnemies();
            if (visibleEnemies.Count > 0)
            {
                if (state == UnitStates.moving)
                {
                    StopMovement();
                }
                StartAiming(visibleEnemies[Random.Range(0, visibleEnemies.Count)]);
                return;
            }
        }
        base.Update();
    }

    bool DoIWantToStop()
    {
        if (DoISeeAnyone())
        {
            return UF.Chance((stats.morale + stopMovingBias) / (100f + stopMovingBias) * Time.deltaTime);
        }
        return UF.Chance((stats.morale + stopMovingBias  + 20f) / (100f + stopMovingBias + 20f) * Time.deltaTime);
    }
    bool DoIWantToMove()
    {
        if (DoISeeAnyone())
        {
            return UF.Chance((stats.morale + startMovingBias + 50f) / (100f + startMovingBias + 50f)  * Time.deltaTime);
        }
        return UF.Chance((stats.morale + startMovingBias) / (100f + startMovingBias) * Time.deltaTime);
    }
    void StartMoving()
    {
        Vector2 newPos = (Vector2)transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(0f, -3f));
        SetMovePoint(newPos);
    }
    bool DoISeeAnyone()
    {
        foreach (GameObject allie in GM.ins.allies)
        {
            if (IsVisible(allie))
            {
                return true;
            }
        }
        return false;
    }
}
