using UnityEngine;

public interface IState 
{
    void StartMoving(Vector2 point);
    void StopMoving();
    void StartAiming(Transform enemy);
    void StopAiming();
    void StartShooting(ShootInfo shootInfo, Transform enemy);
    void StopShooting();
    void GetDown();
    void GetUp();
    void Reload();
    void Update();
    int GetStateId();
}

public enum UnitStates { idle, moving, aiming, shooting, prone };
