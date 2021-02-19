using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityShooting : Entity
{
    [Header("Shoot")]
    [SerializeField] protected ShootConfig shootConfig;
    [SerializeField] protected Transform[] shootPoses;
    protected float fireDelay;

    public void ShootBullet(Transform pos)
    {
        fireDelay = 0;
        SoundManager.Instance.PlaySound(shootConfig.bulletSound, 0.1f, 1.5f);
    }

    public virtual void Shooting()
    {
        fireDelay += Time.deltaTime;
    }

    public override void Update()
    {
        base.Update();
        Shooting();
    }
}
