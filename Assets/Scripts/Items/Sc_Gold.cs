using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Gold : Sc_Item
{
    [SerializeField] int scoreValue = 500;

    public override void Effect(Sc_Entity entity)
    {
        base.Effect(entity);

        if (entity.GetComponent<Sc_ShipController>() && entity.CurrentHealth < entity.MaxHealth)
        {
            Sc_LevelManager.GlobalScore += scoreValue;
            Destroy(gameObject);
        }
    }
}
