using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : SingleShot
{
    public override void Start()
    {
        base.Start();
        type = GunType.pistol;
    }

    public override void Fire()
    {
        base.Fire();
        EjectCasing(); 
    }
}
