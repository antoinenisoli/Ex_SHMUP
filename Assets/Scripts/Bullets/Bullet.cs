using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Item
{
    protected int dmg;

    public void Initialize(int _damage)
    {
        dmg = _damage;
    }

    public override void Effect(Entity entity)
    {
        if (entity.GetComponent<Enemy>())
        {
            entity.ModifyHealth(dmg);
            ship.CurrentEnergy++;
            SoundManager.Instance.PlaySound("Hurt07", 0.1f, 1.5f);
            Destroy(gameObject);
        }
    }

    public override void Update()
    {
        CheckBounds();
    }

    public override void FixedUpdate()
    {
        //don't affect the bullet direction
    }
}
