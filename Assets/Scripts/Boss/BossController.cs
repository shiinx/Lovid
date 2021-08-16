using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class BossController : MonoBehaviour
{
    public FloatVariable health;
    public BossConstants bossConstants;
    public TurretConstants turretConstants;
    public HealthBar healthBar;
    public GameObject bullet;
    public PlayerConstants playerConstants;
    public UnityEvent onBossDeath;
    public GameObject bulletParent;
    private Rigidbody2D bossBody;
    private float maxHealth;
    private float speed;
    private float lineOfSight;
    private float shootingRange; // Less than line of sight
    private float meleeRange;  // Less than shooting range
    private float fireRate;
    private float nextFireTime;
    private float pauseDuration;
    private bool paused;

    private bool isDead;
    private float timer;
    private Vector2 velocity;
    private Transform playerLocation; // This tracks the player's location for the enemy to shoot at him, TODO: Replace this with the turrets
    private Animator bossAnimator;
    private bool dead = false ; 
    public UnityEvent onBossAppear ; 
    public UnityEvent onBossDamage ; 
    public IntVariable bossNumber ; 



    private GameObject[]
        turrets; // This tracks the player's location for the enemy to shoot at him, TODO: Replace this with the turrets

    // Start is called before the first frame update
    private void Start()
    {   
        bossNumber.Value = 0 ; 
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
        bossBody = GetComponent<Rigidbody2D>();
        bossAnimator = this.gameObject.transform.GetChild(3).gameObject.GetComponent<Animator>();
        isDead = false;
        onBossAppear.Invoke() ; 
    }

    // Update is called once per frame
    private void Update()
    {
        if (!paused && !isDead)
        {
            bossAnimator.SetBool("isLow", false);
            velocity = new Vector2(speed, 0);
            var distanceFromPlayer = Vector2.Distance(playerLocation.position, transform.position);
            if (distanceFromPlayer > shootingRange)
            {
                bossAnimator.SetBool("isShooting", false);
                SoundManager.PlaySound("move");
                bossBody.MovePosition(bossBody.position + velocity * Time.fixedDeltaTime);

            }
            else if (distanceFromPlayer <= shootingRange)
            {
                if (nextFireTime < Time.time)
                {
                    bossAnimator.SetBool("isShooting", true);
                    Instantiate(bullet, bulletParent.transform.position, Quaternion.identity); // init bullet
                    SoundManager.PlaySound("shoot");
                    nextFireTime = Time.time + fireRate;
                }
                velocity = new Vector2(speed / 1, 0); // Halved speed while shooting the player
                bossBody.MovePosition(bossBody.position + velocity * Time.fixedDeltaTime);
            }
            else if (distanceFromPlayer <= meleeRange && nextFireTime < Time.time)
            {
                bossAnimator.SetBool("isShooting", false);
                bossAnimator.SetBool("isMelee", true);
                 SoundManager.PlaySound("melee");
                // "ADDITIONAL FEATURE" damage player OR AOE attack (to sync with the team)
                /*
                - Run attack "swipe" animation
                - Decrease player health in state manager
                */
            }
        }
        else
        {
            if (timer >= 0)
            {
                bossAnimator.SetBool("isLow", true);
                timer -= Time.deltaTime;
                // play animation of staggered boss
                // Debug.Log("Frozen!");
            }
            else
            {
                paused = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        if (other.gameObject.tag == "BombTurret") TakeClaymoreDamage();
    }

    public void TakeBulletDamage()
    {
        loseHealth(playerConstants.rangeDamage);
        onBossDamage.Invoke() ; 
    }


    public void TakeTurretDamage()
    {
        loseHealth(turretConstants.attackTurretDamage);
        onBossDamage.Invoke() ; 
    }

    public void TakeClaymoreDamage()
    {
        loseHealth(turretConstants.bombTurretDamage);
        onBossDamage.Invoke() ; 
    }


    private void Move()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime);
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
            print(health);
            paused = true;
            timer = pauseDuration; // start pause timer
            SoundManager.PlaySound("dizzy");
        }
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