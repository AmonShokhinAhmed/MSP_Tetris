using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDebugVisualizer : MonoBehaviour
{
    public Color Color = Color.magenta;
    private void OnDrawGizmos()
    {
        float sideLength = transform.localScale.x;
        Vector3 a = transform.position + Vector3.left * sideLength * 0.5f + Vector3.up * sideLength * 0.5f;
        Vector3 b = a + Vector3.right * sideLength;
        Vector3 c = b + Vector3.down * sideLength;
        Vector3 d = c + Vector3.left * sideLength;
        Gizmos.color = Color;
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, c);
        Gizmos.DrawLine(c, d);
        Gizmos.DrawLine(d, a);
    }
}
