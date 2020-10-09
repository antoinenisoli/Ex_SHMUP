using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sc_ShipController : Sc_EntityShooting
{
    Animator anim => GetComponentInChildren<Animator>();

    [Header("Player controller")]
    public int lifes = 3;
    [SerializeField] float respawnDelay = 2;
    [SerializeField] float camBoundsX = 2;
    [SerializeField] Vector2 camBoundsY = new Vector2(2,5);

    [Header("Player Shooting")]
    [SerializeField] ShootMode shootMode = ShootMode.Semi;
    [SerializeField] Text modeDisplay; 
    [SerializeField] Sc_ShootConfig[] allConfigs;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LineRenderer laserFx;

    [Header("Laser")]
    [SerializeField] Slider energySlider;
    [SerializeField] float currentEnergy;
    [SerializeField] float maxEnergy = 100;
    public float CurrentEnergy
    {
        get => currentEnergy;

        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > MaxEnergy)
                value = MaxEnergy;

            currentEnergy = value;
        }
    }
    public float MaxEnergy { get => maxEnergy; set => maxEnergy = value; }

    float inputY;
    float inputX;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Death()
    {
        Sc_SoundManager.Instance.PlaySound("Explosion010", 0.1f, 1);
        base.Death();
        lifes--;
        
        if (lifes > 0)
            Invoke(nameof(Respawn), respawnDelay);
        else
            Invoke(nameof(Reload), respawnDelay);
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void Respawn()
    {
        anim.SetTrigger("Arrival");
        base.Respawn();
    }

    public void SwitchShootMode(ShootMode mode, float duration)
    {
        shootMode = mode;
        StartCoroutine(ResetShootMode(duration));
    }

    IEnumerator ResetShootMode(float _duration)
    {
        yield return new WaitForSeconds(_duration);
        Sc_SoundManager.Instance.PlaySound("Powering down 01", 0.1f, 1.5f);
        if (shootMode != ShootMode.Semi)
        {
            shootMode = ShootMode.Semi;
        }
    }

    void Move()
    {
        //main player controls
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        //clamp the position into the game view
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -camBoundsX, camBoundsX);
        pos.y = Mathf.Clamp(pos.y, camBoundsY.x, camBoundsY.y);
        transform.position = pos;
    }

    public override void ShootBullet(Transform pos)
    {
        base.ShootBullet(pos);
        GameObject newBullet = Instantiate(shootConfig.bullet, pos.position, Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = Vector2.up * shootConfig.bulletSpeed;
        newBullet.GetComponent<Sc_Bullet>().Initialize(damage);
    }

    public void ShootLaser()
    {
        Sc_SoundManager.Instance.PlaySound(shootConfig.bulletSound, 0.05f, 2.5f);
        float dist = Vector2.Distance(shootPos.position, Vector2.up * camBoundsY.y);
        Debug.DrawRay(shootPos.position, transform.up * dist, Color.red);

        Vector3 length = new Vector3(laserFx.startWidth, 1);
        RaycastHit2D[] hitEnemy = Physics2D.BoxCastAll(shootPos.position, length, 0, transform.up, dist, enemyLayer);
        laserFx.SetPosition(1, new Vector3(shootPos.position.x, camBoundsY.y + 1.5f));

        foreach (RaycastHit2D hit in hitEnemy)
        {
            Sc_Enemy enemy = hit.collider.gameObject.GetComponent<Sc_Enemy>();

            if (enemy && enemy.hitDelay > shootConfig.fireRate)
            {
                enemy.hitDelay = 0;
                enemy.ModifyHealth(damage);
            }
        }
    }

    public override void Shooting()
    {
        base.Shooting();        
        shootConfig = allConfigs[(int)shootMode];
        modeDisplay.text = shootMode.ToString();
        modeDisplay.color = shootConfig.displayColor;
        switch (shootMode)
        {
            case ShootMode.Auto:
                laserFx.SetPosition(0, shootPos.position);
                laserFx.SetPosition(1, shootPos.position);
                if (Input.GetButton("Fire1") && fireDelay > shootConfig.fireRate)
                {
                    ShootBullet(shootPos);
                }
                break;
            case ShootMode.Semi:
                laserFx.SetPosition(0, shootPos.position);
                laserFx.SetPosition(1, shootPos.position);
                if (Input.GetButtonDown("Fire1") && fireDelay > shootConfig.fireRate)
                {
                    ShootBullet(shootPos);
                }
                break;
            case ShootMode.Laser:
                laserFx.SetPosition(0, shootPos.position);
                laserFx.gameObject.SetActive(Input.GetButton("Fire1"));
                if (Input.GetButton("Fire1"))
                {
                    ShootLaser();
                }
                break;
        }

        if (Input.GetButtonDown("Laser") && CurrentEnergy == MaxEnergy)
        {
            Sc_SoundManager.Instance.PlaySound("Loading 04", 0.1f, 1.5f);
            SwitchShootMode(ShootMode.Laser, 5);
            CurrentEnergy = 0;
        }

        energySlider.value = CurrentEnergy;
        energySlider.maxValue = MaxEnergy;
    }

    public override IEnumerator HitFlash(Color newColor)
    {
        if (newColor == Color.red)
        {
            Sc_SoundManager.Instance.PlaySound("Hurt01", 0.15f, 1);
            isHit = true;
        }

        spr.material = hitMaterial;
        Material modifiedMat = spr.material;
        spr.material = modifiedMat;
        modifiedMat.SetColor("_EmissionColor", newColor);
        yield return new WaitForSeconds(hurtDelay);
        isHit = false;
        spr.material = baseMat;
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            Vector2 vel = new Vector2(inputX, inputY);
            rb.velocity = vel.normalized * moveSpeed * Time.deltaTime;
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        base.Update();
        Move();
    }
}
