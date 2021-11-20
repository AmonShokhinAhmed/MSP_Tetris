using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adapted from: https://www.codegrepper.com/code-examples/csharp/safe+area+unity
public class AutoSafeArea : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect prevSafeArea = new Rect(0, 0, 0, 0);

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Rect safeArea = Screen.safeArea;

        if (safeArea != prevSafeArea)
        {
            updateSafeArea(safeArea);
            prevSafeArea = safeArea;
        }
    }

    void updateSafeArea(Rect rect)
    {
        Vector2 anchorMin = rect.position;
        Vector2 anchorMax = rect.position + rect.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
