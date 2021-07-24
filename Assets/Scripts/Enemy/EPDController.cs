using UnityEngine;
using UnityEngine.Events;

public class EPDController : MonoBehaviour, EnemyInterface {
    public EnemyConstants enemyConstants;
    public PlayerConstants playerConstants;
    public TurretConstants turretConstants;
    public PathVariable epdPath;
    public UnityEvent onEnemyTakeDamage;
    public UnityEvent onEnemyDeath;
    public FloatVariable epdHealth;

    private float pathY;
    private Rigidbody2D enemyBody;
    private Vector2 epdVelocity;

    // Start is called before the first frame update
    private void Start() {
        epdHealth.Value = enemyConstants.epdHealth;
        enemyBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update() {
        //compute velocity using floatvariables, enemyconstants
        //move the enemybody accordingly
        MoveEnemy();
    }
    public void ComputeVelocity()
    {
        //retrieve values from pathvariables
        //say moving according to a math function, y = mx+c
        //if epdPath.pathType is linear, epdVelocity = new Vector(x, m)
        // if sine (y=sinx), y = sin(x); epdVelocity = new Vector(stepvalue, y); x += stepvalue
        //x += stepvalue
        if (epdPath.pathType == EnemyConstants.PathType.linear)
        {
            epdVelocity = new Vector2(epdPath.xStepValue, epdPath.yTransform);
        }
        if (epdPath.pathType == EnemyConstants.PathType.sine)
        {
            pathY = epdPath.yTransform * Mathf.Sin(epdPath.xTransform);
            epdVelocity = new Vector2(epdPath.xStepValue, pathY);
            epdPath.XTransform += epdPath.xStepValue;
        }
        
    }
    public void MoveEnemy()
    {
        //compute velocity
        ComputeVelocity();
        enemyBody.MovePosition(enemyBody.position + epdVelocity*Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Bullet") TakeBulletDamage();
        if (other.gameObject.tag == "TurretBullet") TakeTurretDamage();
        if (other.gameObject.tag == "Claymore") TakeClaymoreDamage();
        if(enemyConstants.epdHealth <= 0)
        {
            KillSelf();
            onEnemyDeath.Invoke();
        }
    }
    public void TakeBulletDamage()
    {
        epdHealth.Value -= playerConstants.rangeDamage;
        onEnemyTakeDamage.Invoke();
    }
    public void TakeTurretDamage()
    {
        epdHealth.Value -= turretConstants.attackTurretDamage;
        onEnemyTakeDamage.Invoke();
    }
    public void TakeClaymoreDamage()
    {
        epdHealth.Value -= turretConstants.bombTurretDamage;
        onEnemyTakeDamage.Invoke();
    }
    public void KillSelf()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
    }
}