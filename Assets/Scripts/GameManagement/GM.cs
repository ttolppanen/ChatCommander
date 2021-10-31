using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GM : MonoBehaviour
{
    public static GM ins;
    [SerializeField] Transform navMeshTransform;
    NavMeshSurface2d navMesh;
    public Vector2 mapXSize;
    public Vector2 mapYSize;
    public List<GameObject> enemies;
    public List<GameObject> allies;
    [HideInInspector] public int solidMask;
    public RuntimeAnimatorController[] gunAnimations;
    private void Start()
    {
        if (ins == null)
        {
            ins = this;
        }
        else
        {
            Destroy(gameObject);
        }
        solidMask = LayerMask.GetMask("Enemies", "Allies", "Solid");
        navMesh = navMeshTransform.GetComponent<NavMeshSurface2d>();
    }

    public Vector2 RandomPositionOnMap()
    {
        return new Vector2(Random.Range(mapXSize.x, mapXSize.y), Random.Range(mapYSize.x, mapYSize.y));
    }
    public RuntimeAnimatorController GunAnimation(GunType gunType)
    {
        return gunAnimations[(int)gunType];
    }
    public void UpdateNavMesh()
    {
        StartCoroutine(NavMeshUpdateCoroutine());
    }
    public void SetNavMeshParent(Transform target)
    {
        target.parent = navMeshTransform;
    }
    public void AddToNavMesh(Transform target)
    {
        SetNavMeshParent(target);
        UpdateNavMesh();
    }
    public bool IsInsideMap(Vector2 point)
    {
        return point.x >= mapXSize.x && point.x <= mapXSize.y && point.y >= mapYSize.x && point.y <= mapYSize.y;
    }
    IEnumerator NavMeshUpdateCoroutine()
    {
        yield return new WaitForEndOfFrame();
        navMesh.BuildNavMesh();
    }
    public static void ChangeUnit(GameObject newUnit)
    {

    }
}
