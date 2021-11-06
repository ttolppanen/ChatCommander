using UnityEngine;

public interface IStateContext
{
    void SetState(IState newState);
    T GetComponent<T>();
    Transform GetGunHolder();
    Gun GetGun();
    void ResetGunRotation();
}