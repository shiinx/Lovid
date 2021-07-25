using UnityEngine;

public class ISTDController : MonoBehaviour, EnemyInterface {
    public EnemyConstants enemyConstants;
    public PlayerConstants playerConstants;
    public TurretConstants turretConstants;
    public FloatVariable istdX;
    public FloatVariable istdY;
    public FloatVariable istdYIntercept;

    private Rigidbody2D enemyBody;
    private Vector2 istdVelocity;

    // Start is called before the first frame update
    private void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        istdY.Value = enemyConstants.istdGradient;
        istdX.Value = enemyConstants.istdSpdX;
        istdYIntercept.Value = enemyConstants.istdYIntercept;
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
        istdVelocity = new Vector2(istdX.Value, istdY.Value * istdX.Value + istdYIntercept.Value);
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
    }

    public void TakeBulletDamage()
    {
        enemyConstants.istdHealth -= playerConstants.rangeDamage;
        if(enemyConstants.istdHealth <= 0)
        {
            KillSelf();
        }
    }
    public void TakeTurretDamage()
    {
        enemyConstants.istdHealth -= turretConstants.attackTurretDamage;
        if(enemyConstants.istdHealth <= 0)
        {
            KillSelf();
        }
    }
    public void TakeClaymoreDamage()
    {
        enemyConstants.istdHealth -= turretConstants.bombTurretDamage;
        if(enemyConstants.istdHealth <= 0)
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