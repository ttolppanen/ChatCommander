using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisualEffect
{
    void Play();
    void SetActive(bool val);
    Transform GetTransform();
}
