using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] int scoreValue = 100;
    [SerializeField] float maxPos = -7;
    [HideInInspector] public float hitDelay;

    public override void Update()
    {
        hitDelay += Time.deltaTime;
        base.Update();
        gameObject.SetActive(transform.position.y > maxPos);
    }

    public override void FixedUpdate()
    {
        rb.velocity = Vector2.down * moveSpeed * Time.deltaTime;
    }

    public override void Death()
    {
        LevelManager.Instance.IncreaseScore(scoreValue);
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound("Explosion01", 0.1f, 1);
        base.Death();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Entity _entity = col.GetComponent<Entity>();
        if (_entity != null && _entity.GetComponent<ShipController>())
            _entity.ModifyHealth(damage);
    }
}
