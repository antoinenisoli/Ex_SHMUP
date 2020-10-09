using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Background : MonoBehaviour
{
    [SerializeField] float speed = 0.01f;
    [SerializeField] float maxPos = -8;
    Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position -= Vector3.up * speed * Time.deltaTime;

        if (transform.position.y < maxPos)
        {
            transform.position = startPos;
        }
    }
}
