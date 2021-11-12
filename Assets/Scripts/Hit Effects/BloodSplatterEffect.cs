using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterEffect : HitEffectDisplayer
{
    private void OnEnable()
    {
        UnitHealth.OnBloodSplatterAction += PlayEffect;
    }
    private void OnDisable()
    {
        UnitHealth.OnBloodSplatterAction -= PlayEffect;
    }
}
