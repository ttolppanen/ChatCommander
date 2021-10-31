using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInfo
{
    public float shotTime;
    public int bulletsToShoot;

    public ShootInfo(float time, int bullets)
    {
        shotTime = time;
        bulletsToShoot = bullets;
    }
}
