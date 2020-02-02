using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float rollSpeed;

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float y = material.mainTextureOffset.y;
        y += (rollSpeed / 100.0f) * Time.deltaTime;
        y = Mathf.Repeat(y, 1);
        Vector2 offset = new Vector2(material.mainTextureOffset.x, y);
        material.mainTextureOffset = offset;
    }
}
