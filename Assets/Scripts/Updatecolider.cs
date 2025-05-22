using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updatecolider : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (spriteRenderer.sprite != null)
        {
            polygonCollider.pathCount = spriteRenderer.sprite.GetPhysicsShapeCount();
            for (int i = 0; i < polygonCollider.pathCount; i++)
            {
                var shape = new List<Vector2>();
                spriteRenderer.sprite.GetPhysicsShape(i, shape);
                polygonCollider.SetPath(i, shape.ToArray());
            }
        }
    }
}