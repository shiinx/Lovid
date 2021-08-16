using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FlagController : MonoBehaviour
{
    public FlagConstants flagConstants;
    public FloatVariable flagHealth;
    public BossConstants bossConstants;
    public EnemyConstants enemyConstants;
    public PlayerConstants playerConstants;
    public GameObject redOverlay ; 
    public GameObject helpToolTip ; 
    public float overlayThreshold ; 
    public UnityEvent onFlagTakeDamage;
    public UnityEvent onFlagDeath;
    private bool _isInvincible;
    private bool showed = false ; 
    private AudioSource flagAudioSource ; 

    void Start() {
        redOverlay.SetActive(false) ; 
        flagAudioSource = GetComponent<AudioSource>() ; 
        flagHealth.Value = flagConstants.startingFlagHealth;
        onFlagTakeDamage.Invoke(); 
    }
    
    // TODO: IFrame? Or not
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
                break;
            }
            case "EPDVariant": {
                TakeDamage(enemyConstants.epdVariantTouchDamage);
                break;
            }
            case "EPDFieldVariant": {
                TakeDamage(enemyConstants.epdVariantSkillDamage);
                break;
            }
            case "FreshieVariant": {
                TakeDamage(enemyConstants.freshieVariantTouchDamage);
                break;
            }
        }

        if (flagHealth.Value <= 0) {
            onFlagDeath.Invoke();
        }
    }
    
    private void TakeDamage(float damage) {
        if (_isInvincible) {
            return;
        }

        flagAudioSource.Play() ; 
        flagHealth.Value -= damage;
        onFlagTakeDamage.Invoke();
        if(flagHealth.Value <= flagConstants.startingFlagHealth*overlayThreshold){
            redOverlay.SetActive(true) ; 
            if (!showed){
                showed = true ; 
                StartCoroutine(showHelpText()) ; 
            }
        }
        StartCoroutine(Iframe());

    }
    
    IEnumerator showHelpText(){
        helpToolTip.SetActive(true) ; 
        yield return new WaitForSeconds(3f) ; 
        helpToolTip.SetActive(false) ; 
    }
    
    private IEnumerator Iframe() {
        _isInvincible = true;
        yield return new WaitForSecondsRealtime(flagConstants.iFrameSeconds);
        _isInvincible = false;
    }

}
