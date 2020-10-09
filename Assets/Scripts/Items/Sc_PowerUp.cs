using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PowerUp : Sc_Item
{
    [SerializeField] ShootMode mode;
    [SerializeField] float powerDuration = 5;

    public override void Effect(Sc_Entity entity)
    {
        base.Effect(entity);
        Sc_ShipController ship = entity.GetComponent<Sc_ShipController>();
        if (ship)
        {
            ship.SwitchShootMode(mode, powerDuration);
            Destroy(gameObject);
        }
    }
}
