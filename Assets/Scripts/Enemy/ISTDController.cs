using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class ISTDController : MonoBehaviour, EnemyInterface {
    public bool variant = false;
    public ObjectPointer flagPointer;
    public EnemyConstants enemyConstants;
    public PlayerVariable playerVariable;
    public TurretConstants turretConstants;
    public GameObject coin;

    public UnityEvent onEnemyDeath;

    //public FloatVariable istdHealth;
    private float health;
    NavMeshAgent iagent;

    private bool _isDead;

    // Start is called before the first frame update
    private void Start() {
        iagent = GetComponent<NavMeshAgent>();
        iagent.updateRotation = false;
        iagent.updateUpAxis = false;
        if (!variant) {
            health = enemyConstants.istdHealth;
        } else {
            // increase  or decrease here iagent speed if required 
            health = enemyConstants.istdVariantHelath;
        }
        _isDead = false;
    }

    // Update is called once per frame
    private void Update() {
        iagent.destination = flagPointer.flagTransform.position; //waypoint.transform.position;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        if (iagent.velocity.x < 0 && !variant) {
            transform.localScale = new Vector3(-0.4f, 0.4f, 1);
        } else if (iagent.velocity.x > 0 && !variant){
            transform.localScale = new Vector3(0.4f, 0.4f, 1);
        } else if (iagent.velocity.x < 0 && variant) {
            transform.localScale = new Vector3(-0.6f, 0.6f, 1);
        } else if (iagent.velocity.x > 0 && variant) {
            transform.localScale = new Vector3(0.6f, 0.6f, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("PlayerBullet")) {
            Destroy(other.gameObject);
            TakeBulletDamage();
        }

        if (other.gameObject.CompareTag("TurretBullet")) {
            Destroy(other.gameObject);
            TakeTurretDamage();
        }

        if (other.gameObject.CompareTag("BombTurret")) {
            TakeClaymoreDamage();
        }
        
        if (health <= 0 && !_isDead) {
            _isDead = true;
            onEnemyDeath.Invoke();
            KillSelf();
        }
    }

    public void TakeBulletDamage() {
        health -= playerVariable.PlayerRangeDamage;
    }

    public void TakeTurretDamage() {
        health -= turretConstants.attackTurretDamage;
    }

    public void TakeClaymoreDamage() {
        health -= turretConstants.bombTurretDamage;
    }

    public void KillSelf() {
        float n = variant ? enemyConstants.istdVairantCurrencyValue : enemyConstants.istdCurrencyValue;
        
        for (int i = 0; i < (int) n; i++) {
            Instantiate(coin, this.transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}