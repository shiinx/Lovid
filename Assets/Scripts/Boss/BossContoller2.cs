using UnityEngine;

public class BossContoller2 : MonoBehaviour
{
    public FloatVariable health;
    public BossConstants bossConstants;
    public TurretConstants turretConstants;
    public PlayerConstants playerConstants;

    public HealthBar healthBar;

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

    private Transform playerLocation; // This tracks the player's location for the enemy to shoot at him, TODO: Replace this with the turrets

    private float timer;

    // Start is called before the first frame update
    private void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        health.Value = maxHealth;
        maxHealth = bossConstants.maxHealth;
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
    }

    // Update is called once per frame
    private void Update()
    {
        if (!paused)
        {
            var distanceFromPlayer = Vector2.Distance(playerLocation.position, transform.position);
            if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerLocation.position,
                    speed * Time.deltaTime);
            }
            else if (distanceFromPlayer <= shootingRange && distanceFromPlayer > meleeRange &&
                     nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity); // init bullet
                nextFireTime = Time.time + fireRate;
            }
            else if (distanceFromPlayer <= meleeRange && nextFireTime < Time.time)
            {
                // damage player
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
        if (other.gameObject.tag == "Bullet") TakeBulletDamage();
        if (other.gameObject.tag == "TurretBullet") TakeTurretDamage();
        if (other.gameObject.tag == "Claymore") TakeClaymoreDamage();
    }
    public void TakeBulletDamage()
    {
        loseHealth(playerConstants.rangeDamage);
    }


    public void TakeTurretDamage()
    {
        loseHealth(turretConstants.attackTurretDamage);
    }

    public void TakeClaymoreDamage()
    {
        loseHealth(turretConstants.bombTurretDamage);
    }






    // Upon taking damage
    private void loseHealth(float damage)
    {
        health.Value -= damage;
        if (health.Value <= 0)
            // TODO: Death animation
            Destroy(gameObject); // Dies
        if (health.Value == 0.25 * maxHealth || health.Value == 0.5 * maxHealth || health.Value == 0.75 * maxHealth)
            // stagger boss
            paused = true;
        timer = pauseDuration; // start pause timer
    }


    private void onDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}