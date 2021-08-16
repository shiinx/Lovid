using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;


public class BossContoller2 : MonoBehaviour
{
    public FloatVariable health;
    public IntVariable bossNumber ; 
    public BossConstants bossConstants;
    public TurretConstants turretConstants;
    public PlayerConstants playerConstants;

    public HealthBar healthBar;
    public UnityEvent OnBossAppear ; 
    public UnityEvent OnBossDamage ; 
    public UnityEvent onBossDeath;
    private Rigidbody2D bossBody;
    private float maxHealth;
    private float speed;
    public float lineOfSight;
    public float shootingRange; // Less than line of sight
    public float meleeRange; // Less than shooting range
    public float pauseDuration = 100f;
    public float fireRate = 1f;
    public float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private bool paused;
    private bool isDead;
    private bool dead = false ; 

    private Transform playerLocation; // This tracks the player's location for the enemy to shoot at him, TODO: Replace this with the turrets
    private Animator bossAnimator;
    private float timer;
    private SpriteRenderer mySpriteRenderer;
    private bool isBlocked;

    // Start is called before the first frame update
    private void Start()
    {
        bossNumber.Value = 1 ; 
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        maxHealth = bossConstants.maxHealth;
        health.Value = maxHealth;
        healthBar.SetHealth(health.Value, maxHealth);
        speed = bossConstants.speed;
        lineOfSight = bossConstants.lineOfSight;
        shootingRange = bossConstants.shootingRange; // Less than line of sight
        meleeRange = bossConstants.meleeRange;  // Less than shooting range
        fireRate = bossConstants.fireRate;
        nextFireTime = bossConstants.nextFireTime;
        pauseDuration = bossConstants.pauseDuration;
        paused = false;
        bossBody = GetComponent<Rigidbody2D>();
        bossAnimator = this.gameObject.transform.GetChild(2).gameObject.GetComponent<Animator>();
        isDead = false;
        mySpriteRenderer = this.gameObject.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();
        isBlocked = false;
        OnBossAppear.Invoke() ; 
    }

    // Update is called once per frame
    private void Update()
    {
        if (!paused && !isDead && !isBlocked)
        {
            bossAnimator.SetBool("isLow", false);
            var distanceFromPlayer = Vector2.Distance(playerLocation.position, transform.position);
            if (transform.position.x < playerLocation.position.x)
            {
                // moves to the right
                mySpriteRenderer.flipX = true;
            }
            else
            {
                mySpriteRenderer.flipX = false;
            }
            if (distanceFromPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerLocation.position,
                    -1 * speed * Time.deltaTime * 2);
                bossAnimator.SetBool("isShooting", false);
                SoundManager.PlaySound("move");
            }
            else if (distanceFromPlayer <= shootingRange && distanceFromPlayer > meleeRange &&
                     nextFireTime < Time.time)
            {
                bossAnimator.SetBool("isShooting", true);
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity); // init bullet
                SoundManager.PlaySound("shoot");
                nextFireTime = Time.time + fireRate;
            }
            else if (distanceFromPlayer <= meleeRange && nextFireTime < Time.time)
            {
                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isMelee", true);
                SoundManager.PlaySound("melee");
                // damage player
                /*
                - Run attack "swipe" animation
                - Decrease player health in state manager
                */
            }
        }
        else if (isBlocked)
        {
            print("Blocked!");
            isBlocked = false;
        }
        else
        {
            if (timer >= 0)
            {
                bossAnimator.SetBool("isLow", true);
                timer -= Time.deltaTime;
                // play animation of staggered boss
                Debug.Log("Frozen!");
            }
            else
            {
                paused = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            TakeBulletDamage();
        }
        if (other.gameObject.tag == "TurretBullet")
        {
            Destroy(other.gameObject);
            TakeTurretDamage();
        }
        if (other.gameObject.tag == "BombTurret")
        {
            isBlocked = true;
            TakeClaymoreDamage();
        }
        if (other.gameObject.tag == "AttackTurret")
        {
            isBlocked = true;
            print("I am blocked");
        }
        if (other.gameObject.tag == "DefenseTurret")
        {
            isBlocked = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "AttackTurret")
        {
            isBlocked = true;
            print("I am blocked");
        }
        if (other.gameObject.tag == "DefenseTurret")
        {
            isBlocked = true;
        }
    }

    public void TakeBulletDamage()
    {
        loseHealth(playerConstants.rangeDamage);
        OnBossDamage.Invoke() ; 
    }


    public void TakeTurretDamage()
    {
        loseHealth(turretConstants.attackTurretDamage);
        OnBossDamage.Invoke() ; 
    }

    public void TakeClaymoreDamage()
    {
        loseHealth(turretConstants.bombTurretDamage);
        OnBossDamage.Invoke() ; 
    }






    // Upon taking damage
    private void loseHealth(float damage)
    {
        health.Value -= damage;
        if (health.Value <= 0)
        {
            isDead = true;
            // TODO: Death animation
            //onBossDeath.invoke();
            Debug.Log("Dead!");
            bossAnimator.SetBool("isDead", true);
            SoundManager.PlaySound("death");
            Invoke("Die", 5);
        }
        if (health.Value == 0.5 * maxHealth)
        {
            // stagger boss
            paused = true;
            timer = pauseDuration; // start pause timer
            SoundManager.PlaySound("dizzy");
        }
        Debug.Log(health.value);
    }


    private void Die()
    {
        if (!dead){
            dead = true ; 
            onBossDeath.Invoke();
            Destroy(gameObject); // Dies
        }
    }


    private void onDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}