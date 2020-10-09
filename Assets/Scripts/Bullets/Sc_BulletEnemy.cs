using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_BulletEnemy : Sc_Bullet
{
    public override void Effect(Sc_Entity entity)
    {
        if (entity.GetComponent<Sc_ShipController>())
        {
            entity.ModifyHealth(dmg);
            Destroy(gameObject);
        }
    }
}
