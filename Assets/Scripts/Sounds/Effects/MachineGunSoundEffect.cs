using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunSoundEffect : SoundEffectPlayer
{
    private void OnEnable()
    {
        Automatic.OnMachineGunFireSoundEffectAction += PlayEffect;
    }
    private void OnDisable()
    {
        Automatic.OnMachineGunFireSoundEffectAction -= PlayEffect;
    }
}
