using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatic : SingleShot
{
    public float fireRate;
    public static event Action<Vector2> OnMachineGunFireSoundEffectAction;

    public override void Start()
    {
        base.Start();
        type = GunType.automatic;
    }
    public override void Fire()
    {
        base.Fire();
        EjectCasing();
        OnMachineGunFireSoundEffectAction?.Invoke(transform.position);
    }
}