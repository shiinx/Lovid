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
    public float rangeDamage = 5.0f;
    public float speed = 100f;
    public float upSpeed = 50f;
    public float maxSpeed = 7f;
    public float linearDrag = 10f;
    public float bulletForce = 100f;
    public float constructDistance;
    public int iFrameSeconds = 3;
    
    public int platformBuildCost = 2;
    public float platformMarginSpace;
}