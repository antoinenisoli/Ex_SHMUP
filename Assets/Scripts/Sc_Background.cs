using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Sc_Background : MonoBehaviour
{
    Renderer render => GetComponent<Renderer>();
    [SerializeField] float speed = 1f;

    private void Update()
    {
        render.sortingOrder = -100;
        render.material.mainTextureOffset = Vector2.up * (Time.time * speed % 1);
    }
}
