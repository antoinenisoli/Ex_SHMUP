using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected BoxCollider2D myCollider => GetComponent<BoxCollider2D>();
    protected Rigidbody2D rb => GetComponent<Rigidbody2D>();
    protected SpriteRenderer spr => GetComponentInChildren<SpriteRenderer>();

    [SerializeField] protected Material hitMaterial;
    protected Material baseMat;

    //health
    [Header("Health")]
    [SerializeField] protected int currentHealth;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected float hurtDelay = 0.03f;
    [SerializeField] protected GameObject explosionFX;
    public bool isDead;
    protected bool isHit;
    Vector2 startPos;

    public int CurrentHealth
    {
        get => currentHealth;

        set
        {
            if (value <= 0 && !isDead)
            {
                isDead = true;
                Death();
            }

            if (value < 0)
            {
                value = 0;
            }

            if (value > MaxHealth)
                value = MaxHealth;

            currentHealth = value;
        }
    }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    //

    [SerializeField] protected Color hitColor = Color.red;

    //movements
    [Header("Movements")]
    [SerializeField] protected float moveSpeed = 15;
    [SerializeField] protected int damage = 1;

    public virtual void Awake()
    {
        baseMat = spr.material;
        startPos = transform.position;
        Respawn();
    }

    public virtual void ModifyHealth(int amount)
    {
        if (amount > 0)
        {
            if (isHit)
                return;

            StartCoroutine(HitFlash(hitColor));
        }
        else
        {
            StartCoroutine(HitFlash(Color.green));
        }

        CurrentHealth -= amount;
    }

    public virtual IEnumerator HitFlash(Color newColor)
    {
        spr.material = hitMaterial;
        Material modifiedMat = spr.material;
        spr.material = modifiedMat;
        modifiedMat.SetColor("_EmissionColor", newColor);
        yield return new WaitForSeconds(hurtDelay);
        spr.material = baseMat;
    }

    public virtual void Respawn()
    {
        isDead = false;
        CurrentHealth = MaxHealth;
        transform.position = startPos;
    }

    public virtual void Death()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void Update()
    {
        spr.enabled = !isDead;
        myCollider.enabled = !isDead;

        if (isDead)
            return;
    }
}
