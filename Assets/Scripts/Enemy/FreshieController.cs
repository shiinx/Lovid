using UnityEngine;
using UnityEngine.Events;
public class FreshieController : MonoBehaviour, EnemyInterface {
    public EnemyConstants enemyConstants;
    public PlayerConstants playerConstants;
    public TurretConstants turretConstants;
    public PathVariable freshiePath;
    public UnityEvent onEnemyTakeDamage;
    public UnityEvent onEnemyDeath;
    public FloatVariable freshieHealth;
    private float pathY;
    private Rigidbody2D enemyBody;
    private Vector2 freshieVelocity;
    // Start is called before the first frame update
    private void Start() {
        freshieHealth.Value = enemyConstants.freshieHealth;
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
        //if freshiePath.pathType is linear, freshieVelocity = new Vector(x, m)
        // if sine (y=sinx), y = sin(x); freshieVelocity = new Vector(stepvalue, y); x += stepvalue
        //x += stepvalue
        if (freshiePath.pathType == EnemyConstants.PathType.linear)
        {
            freshieVelocity = new Vector2(freshiePath.xStepValue, freshiePath.yTransform);
        }
        if (freshiePath.pathType == EnemyConstants.PathType.sine)
        {
            pathY = freshiePath.yTransform * Mathf.Sin(freshiePath.xTransform);
            freshieVelocity = new Vector2(freshiePath.xStepValue, pathY);
            freshiePath.XTransform += freshiePath.xStepValue;
        }
        
    }
    public void MoveEnemy()
    {
        //compute velocity
        ComputeVelocity();
        enemyBody.MovePosition(enemyBody.position + freshieVelocity*Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Bullet") TakeBulletDamage();
        if (other.gameObject.tag == "TurretBullet") TakeTurretDamage();
        if (other.gameObject.tag == "Claymore") TakeClaymoreDamage();
        if(enemyConstants.freshieHealth <= 0)
        {
            KillSelf();
            onEnemyDeath.Invoke();
        }
    }
    public void TakeBulletDamage()
    {
        freshieHealth.Value -= playerConstants.rangeDamage;
        onEnemyTakeDamage.Invoke();
    }
    public void TakeTurretDamage()
    {
        freshieHealth.Value -= turretConstants.attackTurretDamage;
        onEnemyTakeDamage.Invoke();
    }
    public void TakeClaymoreDamage()
    {
        freshieHealth.Value -= turretConstants.bombTurretDamage;
        onEnemyTakeDamage.Invoke();
    }
    public void KillSelf()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
    }
}