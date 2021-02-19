using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : EnemyShooting
{
    [Header("Patrol")]
    [SerializeField] GameObject handler;
    [SerializeField] float horizontalSpeed = 3;
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] float spreadRate = 0.1f;
    [SerializeField] int bulletAmount = 3;

    public new void ShootBullet(Transform pos)
    {
        fireDelay = 0;
        SoundManager.Instance.PlaySound(shootConfig.bulletSound, 0.1f, 1.5f);
        Vector2 direction = Vector2.down;
        GameObject newBullet = Instantiate(shootConfig.bullet, pos.position, Quaternion.identity);

        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * shootConfig.bulletSpeed;
        newBullet.GetComponent<Bullet>().Initialize(damage);
    }

    IEnumerator SingleShot(Transform pos)
    {
        fireDelay = 0;
        for (int i = 0; i < bulletAmount; i++)
        {
            fireDelay = 0;
            yield return new WaitForSeconds(spreadRate);
            ShootBullet(pos);
        }
    }

    public new void Shooting()
    {
        fireDelay += Time.deltaTime;
        if (player != null && !player.isDead && fireDelay > shootConfig.fireRate && player.transform.position.y < transform.position.y)
        {
            foreach (Transform pos in shootPoses)
                StartCoroutine(SingleShot(pos));
        }
    }

    public void Patrol(Transform pos, Vector3 newRot)
    {
        float distance = Vector3.Distance(transform.position, pos.position);
        if (distance < 0.1f)
            transform.eulerAngles = newRot;
    }

    public override void FixedUpdate()
    {
        rb.velocity = transform.right * horizontalSpeed * Time.deltaTime;
    }

    public override void Update()
    {
        base.Update();
        Patrol(pos1, new Vector3(0, -180, 0));
        Patrol(pos2, new Vector3(0, 0, 0));
        handler.transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }
}
