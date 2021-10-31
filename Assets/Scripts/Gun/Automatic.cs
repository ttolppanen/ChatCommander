using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatic : SingleShot
{
    public float fireRate;
    public override void Start()
    {
        base.Start();
        type = GunType.automatic;
    }
    public override void Fire()
    {
        base.Fire();
        EjectCasing();
    }
}