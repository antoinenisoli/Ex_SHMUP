using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_EnemyShooting : Sc_Enemy
{
    Sc_ShipController player => FindObjectOfType<Sc_ShipController>();

    [SerializeField] Transform shootPos02;

    public override void ShootBullet(Transform pos)
    {
        base.ShootBullet(pos);
        Vector2 direction = player.transform.position - pos.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        GameObject newBullet = Instantiate(shootConfig.bullet, pos.position, Quaternion.AngleAxis(angle, Vector3.forward));

        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction.normalized * shootConfig.bulletSpeed;
        newBullet.GetComponent<Sc_Bullet>().Initialize(damage);
    }

    public override void Shooting()
    {
        base.Shooting();

        if (player != null && !player.isDead && fireDelay > shootConfig.fireRate && player.transform.position.y < transform.position.y)
        {
            ShootBullet(shootPos);
            ShootBullet(shootPos02);
        }
    }
}
