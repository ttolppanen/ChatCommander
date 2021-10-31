using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltAction : SingleShot
{
    public override void Start()
    {
        base.Start();
        type = GunType.boltAction;
    }
}
