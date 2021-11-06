using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour, ISoundEffect
{
    [SerializeField] AudioClip[] effects;
    public float volume;

}
