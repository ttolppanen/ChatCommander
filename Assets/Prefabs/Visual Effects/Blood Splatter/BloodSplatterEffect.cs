using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterEffect : VisualEffectDisplayer
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
