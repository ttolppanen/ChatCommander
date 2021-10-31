using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBox : MonoBehaviour
{
    public GameObject gun;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<AI>().ChangeGun(gun);
    }
}
