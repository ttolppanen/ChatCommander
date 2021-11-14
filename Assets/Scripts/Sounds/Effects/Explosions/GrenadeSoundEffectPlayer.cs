using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSoundEffectPlayer : SoundEffectPlayer
{
    private void OnEnable()
    {
        UnitHealth.OnBloodSplatterAction += Play;
    }
    private void OnDisable()
    {
        UnitHealth.OnBloodSplatterAction -= Play;
    }

    void Play(Vector2 point, float rot)
    {
        PlayEffect(point);
    }
}
