using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Enemy : Sc_EntityShooting
{
    Sc_LevelManager manager => FindObjectOfType<Sc_LevelManager>();
    [SerializeField] int scoreValue = 100;
    [SerializeField] float maxPos = -7;
    [HideInInspector] public float hitDelay;

    public override void Update()
    {
        hitDelay += Time.deltaTime;
        base.Update();
        gameObject.SetActive(transform.position.y > maxPos);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * moveSpeed * Time.deltaTime;
    }

    public override void Death()
    {
        manager.IncreaseScore(scoreValue);
        gameObject.SetActive(false);
        Sc_SoundManager.Instance.PlaySound("Explosion01", 0.1f, 1);
        base.Death();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Sc_Entity _entity = col.GetComponent<Sc_Entity>();

        if (_entity != null && _entity.GetComponent<Sc_ShipController>())
        {
            _entity.ModifyHealth(damage);
        }
    }
}
