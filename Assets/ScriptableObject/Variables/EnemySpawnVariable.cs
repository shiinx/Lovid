using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnVariable", menuName = "ScriptableObjects/Variables/EnemySpawnVariable", order = 6)]
public class EnemySpawnVariable : ScriptableObject
{
    public float startX;
    public float endX;
    public float startY;
    public float endY;
    // Start is called before the first frame update
    public float StartX
    {
        get =>startX;
    }
    public float EndX
    {
        get => endX;
    }
    public float StartY
    {
        get => startY;
    }
    public float EndY
    {
        get => endY;
    }
    public Vector3 getSpawnPoint()
    {
        Vector3 result = new Vector3(Random.Range(startX, endX), Random.Range(startY, endY), 0);
        return result;
    }
}
