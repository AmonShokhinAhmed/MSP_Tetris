using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpriteManager : MonoBehaviour
{
    private SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    public void SetColor(Color color)
    {
        sprite.color = color;
    }
}
