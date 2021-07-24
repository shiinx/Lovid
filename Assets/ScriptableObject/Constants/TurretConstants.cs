using UnityEngine;

[CreateAssetMenu(fileName = "TurretConstants", menuName = "ScriptableObjects/Constants/TurretConstants")]
public class TurretConstants : ScriptableObject {
    //turret values
    public float attackTurretDamage = 2.0f;
    public float bulletForce = 100f;
    public float attackTurretHealth = 10.0f;
    public int attackTurretBuildCost = 10;
    public float attackTurretMarginSpace;

    public float defenseTurretHealth = 15.0f;
    public int defenseTurretBuildCost = 5;
    public float defenseTurretMarginSpace;
    
    public float bombTurretDamage = 10.0f;
    public int bombTurretBuildCost = 1;
    public float bombTurretMarginSpace;
}