using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotInfo
{
    public float damage { get; set; }
    public Vector2 dir { get; private set; }
    public string shooterName { get; private set; }

    public ShotInfo(float damage, Vector2 dir, string shooterName)
    {
        this.damage = damage;
        this.dir = dir;
        this.shooterName = shooterName;
    }
}
