using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class FreshieController : MonoBehaviour, EnemyInterface {
    public bool variant = false;
    public ObjectPointer flagPointer;
    public EnemyConstants enemyConstants;
    public PlayerVariable playerVariable;
    public TurretConstants turretConstants;
    public GameObject coin;
    
    public UnityEvent onEnemyDeath;

    NavMeshAgent agent;
    private Vector2 freshieVelocity;

    //public FloatVariable freshieHealth;
    private float health;
    private bool _isDead;

    // Start is called before the first frame update
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        if (!variant) {
            health = enemyConstants.freshieHealth;
        } else {
            // increase  or decrease here iagent speed if required 
            health = enemyConstants.freshieVariantHealth;
        }
    }

    // Update is called once per frame
    private void Update() {
        agent.destination = flagPointer.flagTransform.position; //waypoint.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        if (agent.velocity.x < 0 && !variant) {
            transform.localScale = new Vector3(-0.4f, 0.4f, 1);
        } else if (agent.velocity.x > 0 && !variant){
            transform.localScale = new Vector3(0.4f, 0.4f, 1);
        } else if (agent.velocity.x < 0 && variant) {
            transform.localScale = new Vector3(-0.6f, 0.6f, 1);
        } else if (agent.velocity.x > 0 && variant) {
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
        float n = variant ? enemyConstants.freshieVairantCurrencyValue : enemyConstants.freshieCurrencyValue;
        for (int i = 0; i < (int) n; i++) {
            Instantiate(coin, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}