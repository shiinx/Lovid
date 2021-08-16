using UnityEngine;

[CreateAssetMenu(fileName = "WaveConstants", menuName = "ScriptableObjects/Constants/WaveConstants")]
public class WaveConstants : ScriptableObject {
    public int totalNumberOfEnemy;
    public int totalNumberOfFreshie;
    public float firstFreshie;
    public float freshieInterval;
    public int totalNumberOfEPD;
    public float firstEPD;
    public float epdInterval;
    public int totalNumberOfISTD;
    public float firstISTD;
    public float istdInterval;
    
}