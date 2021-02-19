using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream:Assets/Scripts/Sc_Background.cs
[ExecuteInEditMode]
public class Sc_Background : MonoBehaviour
=======
public class Background : MonoBehaviour
>>>>>>> Stashed changes:Assets/Scripts/Background.cs
{
    Renderer render => GetComponent<Renderer>();
    [SerializeField] float speed = 1f;

    private void Update()
    {
<<<<<<< Updated upstream:Assets/Scripts/Sc_Background.cs
        render.sortingOrder = -100;
        render.material.mainTextureOffset = Vector2.up * (Time.time * speed % 1);
=======
        transform.position -= Vector3.up * speed * Time.deltaTime;
        if (transform.position.y < maxPos)
            transform.position = startPos;
>>>>>>> Stashed changes:Assets/Scripts/Background.cs
    }
}
