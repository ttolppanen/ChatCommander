using UnityEngine;

public interface IState 
{
    void StartMoving(Vector2 point);
    void StopMoving();
    void StartAiming(Transform enemy);
    void StopAiming();
    void StartShooting(ShootInfo shootInfo, Transform enemy);
    void StopShooting();
    void Reload();
    void Update();
    int GetStateId();
}
