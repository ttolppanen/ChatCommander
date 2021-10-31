using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : AI
{
    public UnitTypes unitType;
    public override void Awake()
    {
        base.Awake();
        enemies = GM.ins.enemies;
    }

    public override void Update()
    {
        if (anim.GetAnimatorTransitionInfo(0).duration > 0 || InForbbiddenState())
        {
            return;
        }
        if (state == UnitStates.idle && DoIWantToShoot())
        {
            List<GameObject> visibleEnemies = VisibleEnemies();
            if (visibleEnemies.Count > 0)
            {
                StartAiming(visibleEnemies[Random.Range(0, visibleEnemies.Count)]);
                return;
            }
        }
        base.Update();
    }
}
public enum UnitTypes { soldier, engineer, medic}
