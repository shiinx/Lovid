using UnityEngine;

public class EPDController : MonoBehaviour, EnemyInterface {
    public EnemyConstants enemyConstants;
    public PlayerConstants playerConstants;
    public TurretConstants turretConstants;
    public FloatVariable epdX;
    public FloatVariable epdY;
    public FloatVariable epdYIntercept;

    private Rigidbody2D enemyBody;
    private Vector2 epdVelocity;

    // Start is called before the first frame update
    private void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        epdX.Value = enemyConstants.epdSpdX;
        epdY.Value = enemyConstants.epdGradient;
        epdYIntercept.Value = enemyConstants.epdYIntercept;
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
        epdVelocity = new Vector2(epdX.Value, epdY.Value * epdX.Value + epdYIntercept.Value);
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
    }
    public void TakeBulletDamage()
    {
        enemyConstants.epdHealth -= playerConstants.rangeDamage;
        if(enemyConstants.epdHealth <= 0)
        {
            KillSelf();
        }
    }
    public void TakeTurretDamage()
    {
        enemyConstants.epdHealth -= turretConstants.attackTurretDamage;
        if(enemyConstants.epdHealth <= 0)
        {
            KillSelf();
        }
    }
    public void TakeClaymoreDamage()
    {
        enemyConstants.epdHealth -= turretConstants.bombTurretDamage;
        if(enemyConstants.epdHealth <= 0)
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