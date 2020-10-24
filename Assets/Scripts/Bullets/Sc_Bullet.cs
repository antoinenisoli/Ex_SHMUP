using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Bullet : Sc_Item
{
    Sc_ShipController ship => FindObjectOfType<Sc_ShipController>();
    protected int dmg;

    public void Initialize(int _damage)
    {
        dmg = _damage;
    }

    public override void Effect(Sc_Entity entity)
    {
        base.Effect(entity);
        if (entity.GetComponent<Sc_Enemy>())
        {
            entity.ModifyHealth(dmg);
            ship.CurrentEnergy++;
            Sc_SoundManager.Instance.PlaySound("Hurt07", 0.1f, 1.5f);
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
