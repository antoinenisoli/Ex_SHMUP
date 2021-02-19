using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Item
{
    [SerializeField] int healValue = 1;

    public override void Effect(Entity entity)
    {        
        if (entity.GetComponent<ShipController>() && entity.CurrentHealth < entity.MaxHealth)
        {
            SoundManager.Instance.PlaySound("Bip05", 0.4f, 1);
            entity.ModifyHealth(-healValue);
            Destroy(gameObject);
        }
    }
}
