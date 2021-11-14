using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarDisplayer : MonoBehaviour
{
    [SerializeField] float offset = 1;

    Slider hpSlider;
    Transform target;
    IDamageable targetHealth;

    private void Awake()
    {
        hpSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = target.transform.position + offset * Vector3.up;
            hpSlider.value = targetHealth.GetHpPercetage();
            if (hpSlider.value == 1.0f)
            {
                hpSlider.gameObject.SetActive(false);
            }
            else
            {
                hpSlider.gameObject.SetActive(true);
            }
        }
    }

    public void SetFollowing(Transform target)
    {
        this.target = target;
        targetHealth = target.GetComponentInChildren<UnitHealth>();
    }
}
