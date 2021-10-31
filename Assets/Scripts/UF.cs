using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static bool ValidDirection(string dir)
    {
        return dir == "right" || dir == "up" || dir == "left" || dir == "down";
    }
    public static Vector2 DirectionToVector(string dir)
    {
        switch (dir)
        {
            case "right":
                return Vector2.right;
            case "up":
                return Vector2.up;
            case "left":
                return Vector2.left;
        }
        return Vector2.down;
    }
}
