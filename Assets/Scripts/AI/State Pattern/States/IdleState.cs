using UnityEngine;

public class IdleState : IState
{
    readonly IStateContext context;

    public IdleState(IStateContext context)
    {
        this.context = context;
    }

    public void StartMoving(Vector2 point)
    {
        context.SetState(new MovingState(context, point));
    }
    public void StopMoving()
    {
    }
    public void Update()
    {
    }
    public int GetStateId()
    {
        return (int)UnitStates.idle;
    }

    public void StartAiming(Transform enemy)
    {
        context.SetState(new AimingState(context, enemy));
    }

    public void StopAiming()
    {
    }
    public void Reload()
    {
        context.GetGun().Reload();
        context.GetComponent<Animator>().SetTrigger("Reload");
    }

    public void StartShooting(ShootInfo shootInfo, Transform enemy)
    {
        context.SetState(new ShootingState(context, shootInfo, enemy));
    }

    public void StopShooting()
    {
    }
    public void GetDown()
    {
        context.SetState(new ProneState(context));
    }
    public void GetUp()
    {
    }
}