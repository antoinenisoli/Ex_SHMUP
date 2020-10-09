using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Heal : Sc_Item
{
    [SerializeField] int healValue = 1;

    public override void Effect(Sc_Entity entity)
    {
        base.Effect(entity);
        
        if (entity.GetComponent<Sc_ShipController>() && entity.CurrentHealth < entity.MaxHealth)
        {
            Sc_SoundManager.Instance.PlaySound("Bip05", 0.4f, 1);
            entity.ModifyHealth(-healValue);
            Destroy(gameObject);
        }
    }
}
