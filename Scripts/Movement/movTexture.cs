using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movTexture : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();        //Gets the renderer
    }

    //Moves a Material over the area of an Object in a seamless motion
    void Update()
    {
        float moveThis = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, moveThis));
    }
}
