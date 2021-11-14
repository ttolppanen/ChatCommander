using UnityEngine.AI;
using UnityEngine;

public class MovingState : IState
{
    readonly IStateContext context;

    readonly Movement mov;
    readonly NavMeshAgent agent;

    public MovingState(IStateContext context, Vector2 point)
    {
        this.context = context;
        mov = context.GetComponent<Movement>();
        agent = context.GetComponent<NavMeshAgent>();
        mov.Move(point);
    }
    public void StartMoving(Vector2 point)
    {
        mov.Move(point);
    }
    public void StopMoving()
    {
        mov.Stop();
        context.SetState(new IdleState(context));
    }
    public void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    StopMoving();
                    return;
                }
            }
        }
        mov.SetRotation();
    }
    public int GetStateId()
    {
        return (int)UnitStates.moving;
    }

    public void StartAiming(Transform enemy)
    {
        mov.Stop();
        context.SetState(new AimingState(context, enemy));
    }

    public void StopAiming()
    {
    }

    public void Reload()
    {
    }

    public void StartShooting(ShootInfo shootInfo, Transform enemy)
    {
    }

    public void StopShooting()
    {
    }
    public void GetDown()
    {
        mov.Stop();
        context.SetState(new ProneState(context));
    }
    public void GetUp()
    {
    }
}
