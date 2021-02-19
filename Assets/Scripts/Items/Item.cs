using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected ShipController ship;
    [SerializeField] protected float scrollingSpeed = 20;
    [SerializeField] protected float boundX = 2;
    [SerializeField] protected Vector2 boundsY = new Vector2(-0.3f, 5f);

    public void Awake()
    {
        ship = FindObjectOfType<ShipController>();
        rb = GetComponent<Rigidbody2D>();
    }

    public abstract void Effect(Entity entity);

    private void OnTriggerEnter2D(Collider2D col)
    {
        Entity _entity = col.GetComponent<Entity>();
        if (_entity)
        {
            print("waw");
            if (!_entity.isDead)
                Effect(_entity);
        }
    }

    public void CheckBounds()
    {
        if (transform.position.x < -boundX || transform.position.x > boundX || transform.position.y < boundsY.x || transform.position.y > boundsY.y)
            Destroy(gameObject);
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
