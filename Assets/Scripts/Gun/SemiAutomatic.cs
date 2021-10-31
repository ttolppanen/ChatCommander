using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutomatic : SingleShot
{
    public override void Start()
    {
        base.Start();
        type = GunType.semi;
    }
    public override void Fire()
    {
        base.Fire();
        EjectCasing();
    }
}
