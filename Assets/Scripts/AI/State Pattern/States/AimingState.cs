using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingState : IState
{
    IStateContext context;

    Gun gun;
    Transform gunHolder;
    Transform me;
    Transform targetEnemy;
    MoodChecker mood;

    public AimingState(IStateContext context, Transform enemy)
    {
        this.context = context;
        me = context.GetComponent<Transform>();
        mood = context.GetComponent<MoodChecker>();
        gunHolder = context.GetGunHolder();
        gun = context.GetGun();
        targetEnemy = enemy;

        LookAtTarget(targetEnemy.position);
        context.ResetGunRotation();
    }

    public int GetStateId()
    {
        return (int)UnitStates.aiming;
    }

    public void StartAiming(Transform enemy)
    {
    }

    public void StartMoving(Vector2 point)
    {
    }

    public void StopAiming()
    {
        context.ResetGunRotation();
        context.SetState(new IdleState(context));
    }

    public void StopMoving()
    {
    }
    public void Reload()
    {
        gun.Reload();
        me.GetComponent<Animator>().SetTrigger("Reload");
        context.SetState(new IdleState(context));
    }
    public void Update()
    {
        if (targetEnemy == null || !UF.DoISeeYou(me, targetEnemy, true))
        {
            StopAiming();
            return;
        }

        LookAtTarget(targetEnemy.position);

        if (mood.AdjustGun())
        {
            AimGun();
        }
        if (mood.IWantToPullTrigger())
        {
            if (gun.ammoInGun > 0)
            {
                ShootInfo shootInfo = new ShootInfo(0, 1);
                if (gun.type == GunType.automatic)
                {
                    int bulletsToShoot = mood.HowManyBulletsIWantToShoot();
                    bulletsToShoot = Mathf.Min(bulletsToShoot, gun.ammoInGun);
                    shootInfo.bulletsToShoot = bulletsToShoot;
                }
                StartShooting(shootInfo, targetEnemy);
            }
            else
            {
                StopAiming();
                Reload();
            }
            return;
        }
        if (mood.IWantToStopAiming())
        {
            StopAiming();
        }
    }
    void AimGun(float angleFix = -90f)
    {
        float angle = UF.AngleToObject(targetEnemy.position, gunHolder.position);
        angle += mood.AimingError();
        gunHolder.rotation = Quaternion.Euler(0, 0, angle + angleFix);
    }

    public void StartShooting(ShootInfo shootInfo, Transform enemy)
    {
        context.SetState(new ShootingState(context, shootInfo, targetEnemy));
    }

    public void StopShooting()
    {
    }
    public void LookAtTarget(Vector2 target, float angleFix = -90f)
    {
        float angle = UF.AngleToObject(target, me.position);
        me.rotation = Quaternion.Euler(0, 0, angle + angleFix);
    }
    public void GetDown()
    {
        context.ResetGunRotation();
        context.SetState(new ProneState(context));
    }
    public void GetUp()
    {
    }
}
