using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {
    public FloatVariable health;
    public IntVariable buildLimit;
    public Primary equipped;

    public BossConstants bossConstants;
    public EnemyConstants enemyConstants;
    public PlayerConstants playerConstants;

    public UnityEvent onPlayerDeath;
    public UnityEvent onPlayerTakeDamage;

    private void Start() {
        health.Value = playerConstants.maxHealth;
        buildLimit.Value = 0;
        equipped.Value = PlayerConstants.Primary.Gun;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // TODO: Any other Boss attack
        if (other.CompareTag("BossBullet")) {
            health.Value -= bossConstants.rangeDamage;
            onPlayerTakeDamage.Invoke();
        }

        if (other.CompareTag("Boss")) {
            health.Value -= bossConstants.touchDamage;
            onPlayerTakeDamage.Invoke();
        }

        if (other.CompareTag("ISTD")) {
            health.Value -= enemyConstants.istdTouchDamage;
            onPlayerTakeDamage.Invoke();
        }

        if (other.CompareTag("EPD")) {
            health.Value -= enemyConstants.epdTouchDamage;
            onPlayerTakeDamage.Invoke();
        }

        if (other.CompareTag("EPDField")) {
            health.Value -= enemyConstants.epdSkillDamage;
            onPlayerTakeDamage.Invoke();
        }

        if (other.CompareTag("Freshie")) {
            health.Value -= enemyConstants.freshieTouchDamage;
            onPlayerTakeDamage.Invoke();
        }


        if (health.Value <= 0) {
            onPlayerDeath.Invoke();
            Time.timeScale = 0;
        }
    }
}