using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class UF
{
    public static bool Chance(float chance)
    {
        return Random.Range(0f, 1f) < chance;
    }
    public static float AngleToObject(Vector2 b, Vector2 a)
    {
        Vector2 dir = b - a;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
    public static float AngleFromGun(Vector2 playerToEnemy, Vector2 playerToGun, Vector2 gunForward)
    {
        float A = playerToEnemy.x * gunForward.x + playerToEnemy.y * gunForward.y;
        float B = playerToEnemy.y * gunForward.x - playerToEnemy.x * gunForward.y;
        float C = 1 + playerToGun.x * gunForward.x + playerToGun.y * gunForward.y;
        float alpha = Mathf.Atan(A / B);
        return (Mathf.Asin(C / Mathf.Sqrt(A * A + B * B)) - alpha) * Mathf.Rad2Deg;
    }

    public static float SecondDegree(float x, float h, float l, float xl, float c)
    {
        float b = 2 / xl * (h - c + Mathf.Sqrt(h * h - c * h + c * l - l * h));
        float a = b * b / (4 * (l - h));
        return a * x * x + b * x + c;
    }
    public static bool DoISeeYou(Transform me, Transform enemy, bool checkIfBehindCover = false)
    {
        Vector2 dir = enemy.position - me.position;
        RaycastHit2D[] hits;
        if (!checkIfBehindCover)
        {
            hits = Physics2D.RaycastAll(me.position, dir, dir.magnitude, GM.ins.solidMask);
        }
        else
        {
            hits = Physics2D.RaycastAll(me.position, dir, dir.magnitude, GM.ins.solidMask | LayerMask.GetMask("HardToSee"));
        }
        hits = hits.OrderBy(h => h.distance).ToArray();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform == enemy)
            {
                return true;
            }
            else if (hit.transform.CompareTag("Solid"))
            {
                return false;
            }
            else if (hit.transform.CompareTag("HardToSee") && UF.Chance(0.5f))
            {
                return false;
            }
        }
        return false;
    }
    public static bool InPit(Vector3 pos)
    {
        Ray ray = new Ray(pos + new Vector3(0, 0, 10), Vector3.back);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, LayerMask.GetMask("Pit"));
        return hit.collider != null;
    }
}
