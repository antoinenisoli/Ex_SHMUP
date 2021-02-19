using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Item
{
    [SerializeField] int scoreValue = 500;

    public override void Effect(Entity entity)
    {
        ShipController ship = entity.GetComponent<ShipController>();
        if (ship)
        {
            LevelManager.Instance.GlobalScore += scoreValue;
            Destroy(gameObject);
        }
    }
}
