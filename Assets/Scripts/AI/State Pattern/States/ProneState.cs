using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProneState : IState
{
    readonly IStateContext context;

    GameObject soldierPicture;
    GameObject pronePicture;

    public int GetStateId()
    {
        return (int)UnitStates.prone;
    }

    public ProneState(IStateContext context)
    {
        this.context = context;
        soldierPicture = context.GetComponent<Transform>().GetChild(0).gameObject;
        pronePicture = context.GetComponent<Transform>().GetChild(1).gameObject;

        soldierPicture.SetActive(false);
        pronePicture.SetActive(true);
    }

    public void GetDown()
    {
    }

    public void GetUp()
    {
        soldierPicture.SetActive(true);
        pronePicture.SetActive(false);
        context.SetState(new IdleState(context));
    }

    public void Reload()
    {
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
    }

    public void Update()
    {
    }
}
