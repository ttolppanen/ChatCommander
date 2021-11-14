using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : IState
{
    IStateContext context;

    ShootInfo shootInfo;
    Gun gun;
    Transform enemy;

    public ShootingState(IStateContext context, ShootInfo shootInfo, Transform enemy)
    {
        this.context = context;
        this.shootInfo = shootInfo;
        this.enemy = enemy;
        gun = context.GetGun();
        PullTrigger();
    }

    public int GetStateId()
    {
        return (int)UnitStates.shooting;
    }

    public void Reload()
    {
        throw new System.NotImplementedException();
    }

    public void StartAiming(Transform enemy)
    {
    }

    public void StartMoving(Vector2 point)
    {
    }

    public void StartShooting(ShootInfo shootInfo, Transform enemy)
    {
    }

    public void StopAiming()
    {
    }

    public void StopMoving()
    {
    }

    public void StopShooting()
    {
        if (enemy == null)
        {
            context.ResetGunRotation();
            context.SetState(new IdleState(context));
        }
        else
        {
            context.SetState(new AimingState(context, enemy));
        }
    }

    public void GetDown()
    {
    }
    public void GetUp()
    {
    }

    public void Update()
    {
        if (shootInfo.bulletsToShoot > 0)
        {
            if (Time.time - shootInfo.shotTime >= 1f / ((Automatic)gun).fireRate)
            {
                if (gun.ammoInGun <= 0)
                {
                    StopShooting();
                    Reload();
                }
                else
                {
                    PullTrigger();
                }
            }
        }
        else
        {
            StopShooting();
        }
    }
    void PullTrigger()
    {
        gun.Fire();
        shootInfo.shotTime = Time.time;
        shootInfo.bulletsToShoot--;
        context.GetComponent<Animator>().SetTrigger("Recoil");
    }
}
