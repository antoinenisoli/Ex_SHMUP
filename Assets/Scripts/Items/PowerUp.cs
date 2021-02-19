using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Item
{
    [SerializeField] ShootMode mode;
    [SerializeField] float powerDuration = 5;

    public override void Effect(Entity entity)
    {
        ShipController ship = entity.GetComponent<ShipController>();
        if (ship)
        {
            ship.SwitchShootMode(mode, powerDuration);
            Destroy(gameObject);
        }
    }
}
