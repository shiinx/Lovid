using UnityEngine;

[CreateAssetMenu(fileName = "WaveConstants", menuName = "ScriptableObjects/Constants/FlagConstants")]
public class FlagConstants : ScriptableObject {
    public int waitTime;
    public float startingFlagHealth;
    public int iFrameSeconds;

}