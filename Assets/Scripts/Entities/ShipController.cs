using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShipController : EntityShooting
{
<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
    Animator anim => GetComponentInChildren<Animator>();
    Camera mainCam => Camera.main;
    float inputY;
    float inputX;
    [SerializeField] GameObject pauseScreen;
    bool paused;
    Vector3 smoothPosition;
=======
    [SerializeField] Animator anim;
    Camera viewCam;
>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs

    [Header("Player controller")]
    public int lifes = 3;
    [SerializeField] float respawnDelay = 2;
<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
=======
    [SerializeField] Vector2 camBounds = new Vector2(0.5f, 0.5f);
    Vector2 screenBounds;
>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs

    [Header("Player Shooting")]
    [SerializeField] ShootMode shootMode = ShootMode.Auto;
    [SerializeField] Text modeDisplay; 
    [SerializeField] ShootConfig[] allConfigs;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] LineRenderer laserFx;
    float powerUpDuration;

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
                value = 0;

            if (value > MaxEnergy)
                value = MaxEnergy;

            currentEnergy = value;
        }
    }
    public float MaxEnergy { get => maxEnergy; set => maxEnergy = value; }

<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
=======
    float inputY;
    float inputX;

    [SerializeField] GameObject pauseScreen;
    bool paused;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        viewCam = Camera.main;
    }

>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs
    public override void Death()
    {
        SoundManager.Instance.PlaySound("Explosion010", 0.1f, 1);
        base.Death();
        lifes--;
        
        if (lifes > 0)
            Invoke(nameof(Respawn), respawnDelay);
        else
            Invoke(nameof(Reload), respawnDelay);
    }

    public void Reload()
    {
        LevelManager.Instance.GlobalScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void Respawn()
    {
        powerUpDuration = 0;
        anim.SetTrigger("Arrival");
        base.Respawn();
    }

    public void SwitchShootMode(ShootMode mode, float duration)
    {
        shootMode = mode;
<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
        if (mode != ShootMode.Semi)
            powerUpDuration = duration;
        else
            powerUpDuration = 0;
=======
        powerUpDuration = duration;
>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs
    }

    void Move()
    {
        //clamp the position into the game view
        Vector2 pos = transform.position;
<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
        float offset = 0.2f;
        Vector2 screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        screenBounds.x -= offset;
        screenBounds.y -= offset;
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -0.5f + offset, screenBounds.y);
=======
        screenBounds = viewCam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float bottomScreen = screenBounds.y - viewCam.orthographicSize * 2 + camBounds.y;
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x + camBounds.x, screenBounds.x - camBounds.x);
        pos.y = Mathf.Clamp(pos.y, bottomScreen, screenBounds.y - camBounds.y);
>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs
        transform.position = pos;
    }

    public new void ShootBullet(Transform pos)
    {
        base.ShootBullet(pos);
        GameObject newBullet = Instantiate(shootConfig.bullet, pos.position, Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = Vector2.up * shootConfig.bulletSpeed;
        newBullet.GetComponent<Bullet>().Initialize(damage);
    }

    public void ShootLaser()
    {
<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
        Vector2 camBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        Sc_SoundManager.Instance.PlaySound(shootConfig.bulletSound, 0.05f, 2.5f);
        float dist = Vector2.Distance(shootPoses[0].position, Vector2.up * camBounds.y);
=======
        SoundManager.Instance.PlaySound(shootConfig.bulletSound, 0.05f, 2.5f);
        float dist = Vector2.Distance(shootPoses[0].position, Vector2.up * screenBounds.y);
>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs
        Debug.DrawRay(shootPoses[0].position, transform.up * dist, Color.red);

        Vector3 length = new Vector3(laserFx.startWidth, 1);
        RaycastHit2D[] hitEnemy = Physics2D.BoxCastAll(shootPoses[0].position, length, 0, transform.up, dist, enemyLayer);
<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
        laserFx.SetPosition(1, new Vector3(shootPoses[0].position.x, camBounds.y + 1.5f));
=======
        laserFx.SetPosition(1, new Vector3(shootPoses[0].position.x, screenBounds.y + 1.5f));
>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs

        foreach (RaycastHit2D hit in hitEnemy)
        {
            Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
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
        modeDisplay.text = shootConfig.name;
        modeDisplay.color = shootConfig.displayColor;
        laserFx.SetPosition(0, shootPoses[0].position);

        if (shootMode == ShootMode.Laser)
        {
            laserFx.gameObject.SetActive(Input.GetButton("Fire1"));
            if (Input.GetButton("Fire1"))
            {
                ShootLaser();
            }
        }
        else
        {
<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
            laserFx.SetPosition(1, shootPoses[0].position);
            if (Input.GetButton("Fire1") && fireDelay > shootConfig.fireRate)
            {
                ShootBullet(shootPoses[0]);
            }
=======
            case ShootMode.Auto:
                laserFx.SetPosition(0, shootPoses[0].position);
                laserFx.SetPosition(1, shootPoses[0].position);
                if (Input.GetButton("Fire1") && fireDelay > shootConfig.fireRate)
                    ShootBullet(shootPoses[0]);
                break;

            case ShootMode.Laser:
                laserFx.SetPosition(0, shootPoses[0].position);
                laserFx.gameObject.SetActive(Input.GetButton("Fire1"));
                if (Input.GetButton("Fire1"))
                {
                    ShootLaser();
                }
                break;
>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs
        }

        if (Input.GetButtonDown("Laser") && CurrentEnergy == MaxEnergy)
        {
            SoundManager.Instance.PlaySound("Loading 04", 0.1f, 1.5f);
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
            SoundManager.Instance.PlaySound("Hurt01", 0.15f, 1);
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

    public override void FixedUpdate()
    {
        if (isDead)
            rb.velocity = Vector2.zero;
        else
        {
            #if UNITY_ANDROID
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                smoothPosition = Vector3.Lerp(smoothPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition), Time.deltaTime * 6f);
                rb.MovePosition(smoothPosition);
            }

            #elif UNITY_STANDALONE
            Vector2 vel = new Vector2(inputX, inputY);
            rb.velocity = vel.normalized * moveSpeed * Time.deltaTime;

            #endif
        }

        base.FixedUpdate();
    }

<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
    public void PauseButton()
    {
        paused = !paused;
        Time.timeScale = paused ? 0 : 1;
        pauseScreen.SetActive(paused);
    }

    public override void Update()
=======
    void ManageInputs()
>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs
    {
        //main player controls
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        if (Input.GetButtonDown("Cancel") && !paused)
<<<<<<< Updated upstream:Assets/Scripts/Entities/Sc_ShipController.cs
        {
            PauseButton();
        }
        else if (paused && Input.anyKeyDown)
        {
            PauseButton();
        }
=======
            paused = true;
        else if (paused && Input.anyKeyDown)
            paused = false;
    }
>>>>>>> Stashed changes:Assets/Scripts/Entities/ShipController.cs

    public override void Update()
    {
        if (shootMode != ShootMode.Auto)
        {
            powerUpDuration -= Time.deltaTime;

            if (powerUpDuration <= 0)
            {
                shootMode = ShootMode.Auto;
                SoundManager.Instance.PlaySound("Powering down 01", 0.1f, 1.5f);
            }
        }

        base.Update();
        ManageInputs();
        Move();
    }
}
