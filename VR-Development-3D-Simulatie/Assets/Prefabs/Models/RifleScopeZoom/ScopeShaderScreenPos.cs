using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeShaderScreenPos : MonoBehaviour
{
    public Material material;
    
    void Update()
    {
        Vector2 screenPixels = Camera.main.WorldToScreenPoint(transform.position);
        screenPixels = new Vector2(screenPixels.x / Screen.width, screenPixels.y / Screen.height);
        material.SetVector("_ObjectScreenPos", screenPixels);
    }
}
