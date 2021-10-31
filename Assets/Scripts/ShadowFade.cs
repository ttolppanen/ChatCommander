using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class ShadowFade : MonoBehaviour
{
    static int edgeDistance = 10;
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Texture2D originalTexture = transform.parent.GetComponent<SpriteRenderer>().sprite.texture;
        Texture2D texture = new Texture2D(originalTexture.width, originalTexture.height);
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                int amount = AmountOfAlpha(originalTexture, new Vector2Int(x, y));
                texture.SetPixel(x, y, new Color(0, 1, 0, 1));
            }
        }
        texture.Apply();
        sr.sprite = Sprite.Create(originalTexture, sr.sprite.rect, sr.sprite.pivot, sr.sprite.pixelsPerUnit);
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", originalTexture);
        sr.SetPropertyBlock(block);
        print("joo");
    }
    int AmountOfAlpha(Texture2D texture, Vector2Int point)
    {
        int amount = 0;
        for (int y = -edgeDistance; y <= edgeDistance; y++)
        {
            int yPoint = point.y + y;
            if (yPoint >= 0 && yPoint < texture.height)
            {
                for (int x = -edgeDistance; x <= edgeDistance; x++)
                {
                    int xPoint = point.x +x;
                    if (xPoint >= 0 && xPoint < texture.width)
                    {
                        Color col = texture.GetPixel(xPoint, yPoint);
                        if (col.a == 0)
                        {
                            amount++;
                        }
                    }
                }
            }
        }
        return amount;
    }
    float CalcAlpha(int amount)
    {
        float a = (edgeDistance * edgeDistance - amount) / (edgeDistance * edgeDistance);
        return Mathf.Pow(a, 2);
    }
}
