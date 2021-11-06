using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBox : MonoBehaviour
{
    public GameObject newUnit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ChatReader.ins.ChangeUnit(collision.gameObject, newUnit);
        Destroy(gameObject);
    }
}