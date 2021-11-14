using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSoundEffect : SoundEffectPlayer
{
    private void OnEnable()
    {
        Stats.OnLevelUpAction += PlayLevelUpEffect;
    }
    private void OnDisable()
    {
        Stats.OnLevelUpAction -= PlayLevelUpEffect;
    }
    void PlayLevelUpEffect(Vector2 point, int level)
    {
        PlayEffect(point);
    }
}
