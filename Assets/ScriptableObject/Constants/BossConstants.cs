using UnityEngine;

[CreateAssetMenu(fileName = "BossConstants", menuName = "ScriptableObjects/Constants/BossConstants")]
public class BossConstants : ScriptableObject {
    public float maxHealth;
    public float speed;
    public float pauseDuration = 100f;
    
    public float rangeDamage;
    public float lineOfSight;
    public float shootingRange;
    public float fireRate;
    public float nextFireTime;

    public float touchDamage;
    public float meleeRange;
    // TODO: Boss AOE attack
}
