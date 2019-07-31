using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject sprite;
    private Renderer quadRenderer;

    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        quadRenderer = sprite.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 textureOffset = new Vector2(Time.time * scrollSpeed, 0);
        quadRenderer.material.mainTextureOffset = textureOffset;
    }
}
