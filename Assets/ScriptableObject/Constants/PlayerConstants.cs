using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConstants", menuName = "ScriptableObjects/Constants/PlayerConstants")]
public class PlayerConstants : ScriptableObject {
    public enum Primary {
        Gun,
        Platform,
        AttackTurret,
        BombTurret,
        DefenseTurret
    }

    //player values
    public float maxHealth = 50.0f;
    public int startingCurrency = 10;
    public float speed = 100f;
    public float upSpeed = 50f;
    public float maxSpeed = 7f;
    public float linearDrag = 10f;
    public float bulletForce = 100f;
    public float constructDistance;
    public int iFrameSeconds = 3;

    public int damageBlinkFrequncy = 3 ;
    public float timeBetweenDamageBlink = 0.1f  ;  
    
    public int platformBuildCost = 2;
    public float platformMarginSpace;
    
    public float rangeDamage = 2.0f;
    public float shootDistance = 20 ;
    public float firingSpeed = 0.1f;

    public Primary startingPrimary = Primary.Gun;
}