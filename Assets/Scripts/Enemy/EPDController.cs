using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EPDController : MonoBehaviour, EnemyInterface {
    public bool variant = false;
    public ObjectPointer flagPointer;
    public EnemyConstants enemyConstants;
    public PlayerVariable playerVariable;
    public TurretConstants turretConstants;
    public GameObject coin;
    public UnityEvent onEnemyDeath;
    NavMeshAgent eagent;
    private Vector2 epdVelocity;

    //public FloatVariable epdHealth;

    private float health;
    private bool _isDead;

    // Start is called before the first frame update
    private void Start() {
        eagent = GetComponent<NavMeshAgent>();
        eagent.updateRotation = false;
        eagent.updateUpAxis = false;
        if (!variant) {
            health = enemyConstants.epdHealth;
        } else {
            // increase  or decrease here iagent speed if required 
            health = enemyConstants.epdVariantHealth;
        }
    }

    // Update is called once per frame
    private void Update() {
        eagent.destination = flagPointer.flagTransform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        if (eagent.velocity.x < 0 && !variant) {
            transform.localScale = new Vector3(-0.4f, 0.4f, 1);
        } else if (eagent.velocity.x > 0 && !variant){
            transform.localScale = new Vector3(0.4f, 0.4f, 1);
        } else if (eagent.velocity.x < 0 && variant) {
            transform.localScale = new Vector3(-0.6f, 0.6f, 1);
        } else if (eagent.velocity.x > 0 && variant) {
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
        float n = variant ? enemyConstants.epdVairantCurrencyValue : enemyConstants.epdCurrencyValue;
        for (int i = 0; i < (int) n; i++) {
            Instantiate(coin, this.transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}