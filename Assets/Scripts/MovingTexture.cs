using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Slightly offsets the object's main texture each frame.
/// </summary>
public class MovingTexture : MonoBehaviour
{
    // Scroll main texture based on time

    public float scrollSpeed = 1f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
