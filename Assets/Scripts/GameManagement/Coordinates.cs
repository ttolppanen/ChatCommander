using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coordinates : MonoBehaviour
{
    public GameObject coordTextTemplate;

    private void Start()
    {
        InstantiateCoordText(new Vector3(0, 0, 0), "0");
        InstantiateCoordText(new Vector3(GM.ins.mapXSize[1], 0, 0), "x");
        for (int i = 1; i < GM.ins.mapXSize[1]; i++)
        {
            InstantiateCoordText(new Vector3(i, 0, 0), i.ToString());
        }
        InstantiateCoordText(new Vector3(0, GM.ins.mapYSize[1], 0), "y");
        for (int i = 1; i < GM.ins.mapYSize[1]; i++)
        {
            InstantiateCoordText(new Vector3(0, i, 0), i.ToString());
        }
    }

    void InstantiateCoordText(Vector3 pos, string txt)
    {
        GameObject textInst = Instantiate(coordTextTemplate, Camera.main.WorldToScreenPoint(pos), Quaternion.identity);
        textInst.transform.SetParent(transform, true);
        textInst.GetComponent<Text>().text = txt;
    }
}
