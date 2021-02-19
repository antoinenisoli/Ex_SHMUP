using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Enemy
{
    protected ShipController player => FindObjectOfType<ShipController>();
    [Header("Shoot")]
    [SerializeField] protected ShootConfig shootConfig;
    [SerializeField] protected Transform[] shootPoses;
    protected float fireDelay;

    public void ShootBullet(Transform pos)
    {
        Vector2 direction = player.transform.position - pos.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        GameObject newBullet = Instantiate(shootConfig.bullet, pos.position, Quaternion.AngleAxis(angle, Vector3.forward));

        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction.normalized * shootConfig.bulletSpeed;
        newBullet.GetComponent<Bullet>().Initialize(damage);
    }

    public void Shooting()
    {
        if (player != null && !player.isDead && fireDelay > shootConfig.fireRate && player.transform.position.y < transform.position.y)
        {
            foreach (Transform pos in shootPoses)
                ShootBullet(pos);
        }
    }
}
