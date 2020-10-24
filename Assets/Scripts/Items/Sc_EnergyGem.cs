using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_EnergyGem : Sc_Item
{
    [SerializeField] int energyValue = 30;

    public override void Effect(Sc_Entity entity)
    {
        base.Effect(entity);
        Sc_ShipController ship = entity.GetComponent<Sc_ShipController>();
        if (ship && ship.CurrentHealth < ship.MaxHealth)
        {
            ship.CurrentEnergy += energyValue;
            Destroy(gameObject);
        }
    }
}
