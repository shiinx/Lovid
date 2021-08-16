using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour {
    public BossConstants bossConstants;
    public EnemyConstants enemyConstants;
    public PlayerConstants playerConstants;
    public PlayerVariable playerVariable;
    public UnityEvent onPlayerDeath;
    public UnityEvent onPlayerTakeDamage;
    public UnityEvent onPlayerPickCoin;
    public UnityEvent onUIValueChange;
    public UnityEvent onEquipChange;
    public UnityEvent onStatusReset;

    public UnityEvent onConfuseDebuff;
    public UnityEvent onSilenceDebuff;
    public UnityEvent onShootingDebuff;
    public UnityEvent onTurretDebuff;

    private bool _isInvincible;
    public GameObject gun;

    private bool _isConfused;
    private bool _isSilenced;
    private bool _isWeakened;
    private bool _isSleeping;
    private Color _color ; 
    private SpriteRenderer _spriteRenderer ; 
    
    private void Start() {
        _color = GetComponent<SpriteRenderer>().color;
        _spriteRenderer = GetComponent<SpriteRenderer>() ; 
        ResetResponse(); 
    }

    private void OnTriggerStay2D(Collider2D other) {
        switch (other.tag) {
            case "BossBullet": {
                TakeDamage(bossConstants.rangeDamage);
                break;
            }
            case "Boss": {
                TakeDamage(bossConstants.touchDamage);
                break;
            }
            case "ISTD": {
                TakeDamage(enemyConstants.istdTouchDamage);
                break;
            }
            case "EPD": {
                TakeDamage(enemyConstants.epdTouchDamage);
                break;
            }
            case "EPDField": {
                TakeDamage(enemyConstants.epdSkillDamage);
                break;
            }
            case "Freshie": {
                TakeDamage(enemyConstants.freshieTouchDamage);
                break;
            }
            case "ISTDVariant": {
                TakeDamage(enemyConstants.istdVariantTouchDamage);
                ApplyDebuff(other.tag);
                break;
            }
            case "EPDVariant": {
                TakeDamage(enemyConstants.epdVariantTouchDamage);
                ApplyDebuff(other.tag);
                break;
            }
            case "EPDFieldVariant": {
                TakeDamage(enemyConstants.epdVariantSkillDamage);
                ApplyDebuff(other.tag);
                break;
            }
            case "FreshieVariant": {
                TakeDamage(enemyConstants.freshieVariantTouchDamage);
                ApplyDebuff(other.tag);
                break;
            }
            case "EnemyCoin": {
                CollectCoin(other);
                break;
            }
        }

        if (playerVariable.PlayerHealth <= 0) {
            gun.SetActive(false);
            onPlayerDeath.Invoke();
        }
    }

    private void TakeDamage(float damage) {
        if (_isInvincible) {
            return;
        }
        playerVariable.PlayerHealth -= damage;
        onPlayerTakeDamage.Invoke();
        StartCoroutine(Iframe());
        StartCoroutine(OnPlayerDamageBlink()) ; 
    }

    private IEnumerator OnPlayerDamageBlink(){
        // color.a = 1 ; 
        // spriteRenderer.color = color;
        for (int i = 0; i < playerConstants.damageBlinkFrequncy; i++)
        {
            if(!_isInvincible){
                yield break ; 
            }
            _color.a = 0.2f ; 
            _spriteRenderer.color = _color;
            yield return new WaitForSeconds(playerConstants.timeBetweenDamageBlink) ; 
            _color.a = 1f ; 
            _spriteRenderer.color = _color;
            yield return new WaitForSeconds(playerConstants.timeBetweenDamageBlink) ; 
        }
    }

    private void ApplyDebuff(string otherTag) {
        switch (otherTag) {
            case "ISTDVariant": {
                if (!_isWeakened) {
                    onShootingDebuff.Invoke();
                    StartCoroutine(Weakened());
                }
                break;
            }
            case "EPDVariant":
            case "EPDFieldVariant": {
                var r = Random.Range(0, 2);
                if (r == 0 && !_isSilenced && !_isSleeping) {
                    onSilenceDebuff.Invoke();
                    StartCoroutine(Silence());
                } else if(r == 1 && !_isSleeping && !_isSilenced) {
                    onTurretDebuff.Invoke();
                    print("invoke turretdebuff");
                    StartCoroutine(Sleep());
                }
                break;
            }
            case "FreshieVariant": {
                if (!_isConfused) {
                    onConfuseDebuff.Invoke();
                    StartCoroutine(Confused());
                }
                break;
            }
        }
    }

    private void CollectCoin(Collider2D other) {
        Destroy(other.gameObject) ; 
        playerVariable.PlayerCurrency += 1;
        onPlayerPickCoin.Invoke();
    }
    
    private IEnumerator Iframe() {
        _isInvincible = true;
        yield return new WaitForSecondsRealtime(playerConstants.iFrameSeconds);
        _isInvincible = false;
    }
    
    private IEnumerator Confused() {
        playerVariable.PlayerConfused = -1;
        _isConfused = true;
        yield return new WaitForSecondsRealtime(enemyConstants.confuseDebuffTime);
        playerVariable.PlayerConfused = 1;
        _isConfused = false;
    }
    
    private IEnumerator Sleep() {
        playerVariable.PlayerTurretDebuffed = true;
        _isSleeping = true;
        yield return new WaitForSecondsRealtime(enemyConstants.turretDebuffTime);
        playerVariable.PlayerTurretDebuffed = false;
        _isSleeping = false;
    }
    
    private IEnumerator Silence() {
        playerVariable.PlayerSilenced = true;
        _isSilenced = true;
        yield return new WaitForSecondsRealtime(enemyConstants.silenceDebuffTime);
        playerVariable.PlayerSilenced = false;
        _isSilenced = false;
    }
    
    private IEnumerator Weakened() {
        playerVariable.PlayerFiringDelay *= 2f;
        playerVariable.PlayerRangeDamage *= 0.5f;
        playerVariable.PlayerShootDistance *= 0.5f;
        _isWeakened = true;
        yield return new WaitForSecondsRealtime(enemyConstants.confuseDebuffTime);
        playerVariable.PlayerRangeDamage = playerConstants.rangeDamage;
        playerVariable.PlayerFiringDelay = playerConstants.firingSpeed;
        playerVariable.PlayerShootDistance = playerConstants.shootDistance; 
        _isWeakened = false;
    }

    public void ResetResponse() {
        // Init player variables
        playerVariable.PlayerHealth = playerConstants.maxHealth;
        playerVariable.PlayerEquipped = playerConstants.startingPrimary;
        playerVariable.PlayerCurrency = playerConstants.startingCurrency;
        playerVariable.PlayerRangeDamage = playerConstants.rangeDamage;
        playerVariable.PlayerFiringDelay = playerConstants.firingSpeed;
        playerVariable.PlayerShootDistance = playerConstants.shootDistance;
        playerVariable.AttackTurretDelayed = false;
        playerVariable.DefenseTurretDelayed = false;
        playerVariable.BombTurretDelayed = false;
        playerVariable.PlayerSilenced = false;
        playerVariable.PlayerConfused = 1;
        playerVariable.PlayerSilenced = false;
        playerVariable.PlayerEquipped = PlayerConstants.Primary.Gun;
        _isConfused = false;
        _isSleeping = false;
        _isSilenced = false;
        _isWeakened = false;
        
        onUIValueChange.Invoke();
        onStatusReset.Invoke();
        onEquipChange.Invoke();
    }

}