using UnityEngine;
using UnityEngine.Events;
public class ISTDController : MonoBehaviour, EnemyInterface {
    public EnemyConstants enemyConstants;
    public PlayerConstants playerConstants;
    public TurretConstants turretConstants;
    public PathVariable istdPath;
    public UnityEvent onEnemyTakeDamage;
    public UnityEvent onEnemyDeath;
    public FloatVariable istdHealth;

    private float pathY;
    private Rigidbody2D enemyBody;
    private Vector2 istdVelocity;

    // Start is called before the first frame update
    private void Start() {
        istdHealth.Value = enemyConstants.istdHealth;
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
        //if istdPath.pathType is linear, istdVelocity = new Vector(x, m)
        // if sine (y=sinx), y = sin(x); istdVelocity = new Vector(stepvalue, y); x += stepvalue
        //x += stepvalue
        if (istdPath.pathType == EnemyConstants.PathType.linear)
        {
            istdVelocity = new Vector2(istdPath.xStepValue, istdPath.yTransform);
        }
        if (istdPath.pathType == EnemyConstants.PathType.sine)
        {
            pathY = istdPath.yTransform * Mathf.Sin(istdPath.xTransform);
            istdVelocity = new Vector2(istdPath.xStepValue, pathY);
            istdPath.XTransform += istdPath.xStepValue;
        }
        
    }
    public void MoveEnemy()
    {
        //compute velocity
        ComputeVelocity();
        enemyBody.MovePosition(enemyBody.position + istdVelocity*Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Bullet") TakeBulletDamage();
        if (other.gameObject.tag == "TurretBullet") TakeTurretDamage();
        if (other.gameObject.tag == "Claymore") TakeClaymoreDamage();
        if(enemyConstants.istdHealth <= 0)
        {
            KillSelf();
            onEnemyDeath.Invoke();
        }
    }
    public void TakeBulletDamage()
    {
        istdHealth.Value -= playerConstants.rangeDamage;
        onEnemyTakeDamage.Invoke();
    }
    public void TakeTurretDamage()
    {
        istdHealth.Value -= turretConstants.attackTurretDamage;
        onEnemyTakeDamage.Invoke();
    }
    public void TakeClaymoreDamage()
    {
        istdHealth.Value -= turretConstants.bombTurretDamage;
        onEnemyTakeDamage.Invoke();
    }
    public void KillSelf()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
    }
}