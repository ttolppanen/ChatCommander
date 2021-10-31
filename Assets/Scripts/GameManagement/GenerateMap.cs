using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject tree;
    public GameObject rock;
    public int treeAmount;
    public int rockAmount;

    private void Start()
    {
        int spawnTrees = Random.Range(0, treeAmount);
        int spawnRocks = Random.Range(0, rockAmount);
        for (int i = 0; i < spawnTrees; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(GM.ins.mapXSize.x, GM.ins.mapXSize.y), Random.Range(GM.ins.mapYSize.x, GM.ins.mapYSize.y));
            GameObject treeInst = Instantiate(tree, spawnPosition, Quaternion.identity);
            float treeScale = Random.Range(1f, 3f);
            treeInst.transform.localScale = new Vector3(treeScale, treeScale, 1);
            GM.ins.SetNavMeshParent(treeInst.transform);
        }
        for (int i = 0; i < spawnRocks; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(GM.ins.mapXSize.x, GM.ins.mapXSize.y), Random.Range(GM.ins.mapYSize.x, GM.ins.mapYSize.y));
            GameObject rockInst = Instantiate(rock, spawnPosition, Quaternion.identity);
            float rockScale = Random.Range(0.8f, 2f);
            rockInst.transform.localScale = new Vector3(rockScale, rockScale, 1);
            GM.ins.SetNavMeshParent(rockInst.transform);
        }
        GM.ins.UpdateNavMesh();
    }
}
