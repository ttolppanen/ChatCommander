using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootable
{
    bool OnHit(ShotInfo shot, Vector2 hitPosition);
}
