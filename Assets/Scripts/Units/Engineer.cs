using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engineer : MonoBehaviour
{
    public GameObject sandBags;
    public Text resourceText;
    public int sandBagsCost;
    public int resources;

    private void OnEnable()
    {
        //UnitCommands.OnBuildSandBagAction += BuildSandBags;
    }
    bool SpendResources(int amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            UpdateResourceText();
            return true;
        }
        return false;
    }
    public void BuildSandBags(string name, string direction)
    {
        if (gameObject.name == name)
        {
            if (SpendResources(sandBagsCost))
            {
                BuildInstantiateInfo buildInfo = new BuildInstantiateInfo(direction);
                GameObject inst = Instantiate(sandBags, new Vector3(transform.position.x, transform.position.y, sandBags.transform.position.z) + (Vector3)buildInfo.spawnPoint * 0.5f, buildInfo.spawnRotation);
                GM.ins.AddToNavMesh(inst.transform);
            }
        }
    }
    public void GiveResources(int amount)
    {
        resources += amount;
        UpdateResourceText();
    }

    void UpdateResourceText()
    {
        resourceText.text = resources.ToString();
    }
}

public class BuildInstantiateInfo
{
    public Vector2 spawnPoint;
    public Quaternion spawnRotation;

    public BuildInstantiateInfo(string dir)
    {
        Vector2 spawnPoint = Vector2.zero;
        Vector3 spawnRot = Vector3.zero;
        switch (dir)
        {
            case "right":
                spawnPoint += Vector2.right;
                spawnRot = new Vector3(0, 0, -90);
                break;
            case "up":
                spawnPoint += Vector2.up;
                spawnRot = new Vector3(0, 0, 0);
                break;
            case "left":
                spawnPoint += Vector2.left;
                spawnRot = new Vector3(0, 0, 90);
                break;
            case "down":
                spawnPoint += Vector2.down;
                spawnRot = new Vector3(0, 0, 180);
                break;
        }
        this.spawnPoint = spawnPoint;
        this.spawnRotation = Quaternion.Euler(spawnRot);
    }
}
