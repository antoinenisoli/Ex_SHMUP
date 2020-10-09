using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_EntityShooting : Sc_Entity
{
    //shoot
    [Header("Shoot")]
    [SerializeField] protected Sc_ShootConfig shootConfig;
    [SerializeField] protected Transform shootPos;
    protected float fireDelay;

    public virtual void ShootBullet(Transform pos)
    {
        fireDelay = 0;
        Sc_SoundManager.Instance.PlaySound(shootConfig.bulletSound, 0.1f, 1.5f);
    }

    public virtual void Shooting()
    {
        fireDelay += Time.deltaTime;
    }

    public virtual void Update()
    {
        spr.enabled = !isDead;
        myCollider.enabled = !isDead;

        if (isDead)
            return;

        Shooting();
    }
}
