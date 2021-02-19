using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGem : Item
{
    [SerializeField] int energyValue = 30;

    public override void Effect(Entity entity)
    {
        ShipController ship = entity.GetComponent<ShipController>();
        if (ship && ship.CurrentHealth < ship.MaxHealth)
        {
            ship.CurrentEnergy += energyValue;
            Destroy(gameObject);
        }
    }
}
