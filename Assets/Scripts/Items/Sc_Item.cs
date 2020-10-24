using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Sc_Item : MonoBehaviour
{
    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [SerializeField] protected float scrollingSpeed = 20;
    [SerializeField] protected float boundX = 2;
    [SerializeField] protected Vector2 boundsY = new Vector2(-0.3f, 5f);

    public virtual void Effect(Sc_Entity entity)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Sc_Entity _entity = col.GetComponent<Sc_Entity>();

        if (_entity)
        {
            if (_entity.isDead)
                return;

            Effect(_entity);
        }
    }

    public void CheckBounds()
    {
        if (transform.position.x < -boundX || transform.position.x > boundX || transform.position.y < boundsY.x || transform.position.y > boundsY.y)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Update()
    {
        CheckBounds();
    }

    public virtual void FixedUpdate()
    {
        rb.velocity = Vector2.down * scrollingSpeed * Time.deltaTime;
    }
}
