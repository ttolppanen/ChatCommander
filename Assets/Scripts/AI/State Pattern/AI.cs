using UnityEngine;
using System.Collections.Generic;

public class AI : MonoBehaviour, IStateContext
{
    [SerializeField] Transform gunHolder;
    IState currentState;
    Animator anim;
    public MoodChecker mood { get; private set; }
    [HideInInspector] public List<GameObject> enemiesReference;

    public virtual void Awake()
    {
        currentState = new IdleState(this);
        mood = gameObject.AddComponent<MoodChecker>();
        anim = GetComponent<Animator>();
        gameObject.AddComponent<Movement>();
    }

    public void StartMoving(Vector2 point) => currentState.StartMoving(point);
    public void StopMoving() => currentState.StopMoving();
    public void StartAiming(Transform enemy) => currentState.StartAiming(enemy);
    public void StopAiming() => currentState.StopAiming();
    public void StartShooting(ShootInfo shootInfo, Transform enemy) => currentState.StartShooting(shootInfo, enemy);
    public void StopShooting() => currentState.StopShooting();
    public void GetDown() => currentState.GetDown();
    public void GetUp() => currentState.GetUp();
    public void Reload() => currentState.Reload();
    public int GetStateId() => currentState.GetStateId();

    public virtual void Update()
    {
        if (InForbiddenAnimation()) return;
        currentState.Update();
    }

    void IStateContext.SetState(IState newState)
    {
        anim.SetInteger("State", newState.GetStateId());
        currentState = newState;
    }
    T IStateContext.GetComponent<T>()
    {
        return GetComponent<T>();
    }
    Transform IStateContext.GetGunHolder()
    {
        return gunHolder;
    }
    Gun IStateContext.GetGun()
    {
        return gunHolder.GetChild(0).GetComponent<Gun>();
    }
    void IStateContext.ResetGunRotation()
    {
        gunHolder.localRotation = Quaternion.Euler(0, 0, 0);
    }
    bool InForbiddenAnimation()
    {
        return anim.IsInTransition(0) || anim.GetCurrentAnimatorStateInfo(0).IsName("Reloading") || anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot");
    }
    public List<GameObject> VisibleEnemies()
    {
        List<GameObject> visible = new List<GameObject>();
        foreach (GameObject enemy in enemiesReference)
        {
            if (UF.DoISeeYou(transform, enemy.transform, true))
            {
                visible.Add(enemy);
            }
        }
        return visible;
    }
}