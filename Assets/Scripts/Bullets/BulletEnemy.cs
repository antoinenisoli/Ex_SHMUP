using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Bullet
{
    public override void Effect(Entity entity)
    {
        if (entity.GetComponent<ShipController>())
        {
            entity.ModifyHealth(dmg);
            Destroy(gameObject);
        }
    }
}
