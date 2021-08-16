using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConstants", menuName = "ScriptableObjects/Constants/EnemyConstants")]
public class EnemyConstants : ScriptableObject {
    public enum PathType {
        linear = 0,
        sine = 1
    }
    //freshie
    public float freshieHealth = 2.0f;
    public float freshieTouchDamage = 2.0f;    
    public float freshieCurrencyValue = 1.0f ; 
    // vairant 
    public float freshieVariantHealth = 4.0f ; 
    public float freshieVariantTouchDamage = 4.0f ; 
    public float confuseDebuffTime = 10.0f ; 
    public float freshieVairantCurrencyValue = 2.0f ; 


    // istd 
    public float istdHealth = 3.0f;
    public float istdTouchDamage = 4.0f;
    public float istdCurrencyValue = 2.0f ; 
    // istd-variant 
    public float istdVariantHelath = 6.0f ; 
    public float istdVariantTouchDamage = 8.0f ; 
    public float istdVairantCurrencyValue = 4.0f ; 
    public float shootingDebuffTime = 10.0f ; 


    // epd 
    public float epdHealth = 5.0f;
    public float epdTouchDamage = 2.0f;
    public float epdSkillDamage = 1.0f;
    public float epdCurrencyValue = 3.0f ; 
    // epd-variant 
    public float epdVariantHealth = 10.0f ; 
    public float epdVariantTouchDamage = 5.0f ; 
    public float epdVariantSkillDamage = 3.0f ; 
    public float epdVairantCurrencyValue = 6.0f ; 
    public float turretDebuffTime = 10.0f ; 
    public float silenceDebuffTime= 10.0f ; 



    // some random thing 
    public float groundDistance = -4.5f;

}