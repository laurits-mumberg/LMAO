using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TextureScale : MonoBehaviour
{
    // Scroll main texture based on time

    float scrollSpeed = 0.5f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Animates main texture scale in a funky way!
        float scaleX = Mathf.Cos(Time.time) * 0.5f + 1;
        float scaleY = Mathf.Sin(Time.time) * 0.5f + 1;
        rend.material.SetTextureScale("_MainTex", new Vector2(scaleX, scaleY));
    }
}
