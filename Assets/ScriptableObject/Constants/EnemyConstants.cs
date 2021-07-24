using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConstants", menuName = "ScriptableObjects/Constants/EnemyConstants")]
public class EnemyConstants : ScriptableObject {
    //for enemy
    public float freshieHealth = 2.0f;
    public float freshieTouchDamage = 2.0f;
    public float freshieSpdX = -1.0f;
    public float freshieGradient = 0.0f;
    public float freshieYIntercept = 0.0f;
    
    public float istdHealth = 3.0f;
    public float istdTouchDamage = 4.0f;
    public float istdSpdX = -1.0f;
    public float istdGradient = 0.0f;
    public float istdYIntercept = 0.0f;

    public float epdHealth = 5.0f;
    public float epdTouchDamage = 2.0f;
    public float epdSkillDamage = 1.0f;
    public float epdSpdX = -1.0f;
    public float epdGradient = 0.0f;
    public float epdYIntercept = 0.0f;

    public float enemyPatrolTime = 2.0f;
    
    public float groundSurface = -4.5f;
    public float groundDistance = -4.5f;

    // for Reset values
    private Vector3 freshieSpawnPointStart = new Vector3(10.0f, -3.5f, 0); // hardcoded location
}