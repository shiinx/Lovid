using UnityEngine;

public class FreshieController : MonoBehaviour, EnemyInterface {
    public EnemyConstants enemyConstants;
    public PlayerConstants playerConstants;
    public TurretConstants turretConstants;
    public FloatVariable freshieX;
    public FloatVariable freshieY;
    public FloatVariable freshieYIntercept;

    private Rigidbody2D enemyBody;
    private Vector2 freshieVelocity;
    // Start is called before the first frame update
    private void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        freshieY.Value = enemyConstants.freshieGradient;
        freshieX.Value = enemyConstants.freshieSpdX;
        freshieYIntercept.Value = enemyConstants.freshieYIntercept;
    }

    // Update is called once per frame
    private void Update() {
        //compute velocity using floatvariables, enemyconstants
        //move the enemybody accordingly
        MoveEnemy();
    }
    public void ComputeVelocity()
    {
        //retrieve values from floatvariables and enemyconstant
        //say moving according to a math function, y = mx+c
        //epdVelocity = new Vector2(stepValueX, m*stepValueX+c)
        freshieVelocity = new Vector2(freshieX.Value, freshieY.Value * freshieX.Value + freshieYIntercept.Value);
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
    }

    public void TakeBulletDamage()
    {
        enemyConstants.freshieHealth -= playerConstants.rangeDamage;
        if(enemyConstants.freshieHealth <= 0)
        {
            KillSelf();
        }
    }
    public void TakeTurretDamage()
    {
        enemyConstants.freshieHealth -= turretConstants.attackTurretDamage;
        if(enemyConstants.freshieHealth <= 0)
        {
            KillSelf();
        }
    }
    public void TakeClaymoreDamage()
    {
        enemyConstants.freshieHealth -= turretConstants.bombTurretDamage;
        if(enemyConstants.freshieHealth <= 0)
        {
            KillSelf();
        }
    }
    public void KillSelf()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
    }
}