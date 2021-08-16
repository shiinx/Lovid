using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public enum EnemyTypeInWave {
    freshie = 0,
    istd = 1,
    epd = 2
}


[Serializable]
public class WavePoolItem {
    public WaveConstants waveConstant;
    public GameStateVariable waveState;
    public bool spawned = false;
    //public bool expandPool;
    //public EnemyTypeInWave type;
    
}



public class ExistingWavePoolItem {
    public GameObject gameObject;
    public EnemyTypeInWave type;
    public GameStateVariable waveState;

    public ExistingWavePoolItem(GameObject gameObject, EnemyTypeInWave type, GameStateVariable waveState) {
        this.gameObject = gameObject;
        this.type = type;
        this.waveState = waveState;
    }
}

public class WavePooler : MonoBehaviour {
    public static WavePooler SharedInstance;
    public EnemySpawnVariable bottomSpawn;
    public EnemySpawnVariable leftSpawn;
    public EnemySpawnVariable rightSpawn;
    public GameObject freshiePrefab;
    public GameObject istdPrefab;
    public GameObject epdPrefab;
    public GameStateVariable currentGameState;
    public List<WavePoolItem> wavesToPool;
    public List<ExistingWavePoolItem> pooledObjects;
    private List<Vector3> spawnPointList;

    private List<float> enemyTimers;
    private List<float> enemyIntervals;

    private void Awake() {
        SharedInstance = this;
        pooledObjects = new List<ExistingWavePoolItem>();
        spawnPointList = new List<Vector3>();
        spawnPointList.Add(bottomSpawn.getSpawnPoint());
        spawnPointList.Add(leftSpawn.getSpawnPoint());
        spawnPointList.Add(rightSpawn.getSpawnPoint());
    }

    // Start is called before the first frame update
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
        foreach (var wave in wavesToPool) {
            if (currentGameState == wave.waveState && !wave.spawned)
            {
                for (int i = 0; i < wave.waveConstant.totalNumberOfFreshie; i++)
            {
                //bottomSpawnPoint = new Vector3(Random.Range(bottomSpawn.startX, bottomSpawn.endX), Random.Range(bottomSpawn.startY,bottomSpawn.endY), 0);
                RefreshSpawnList();
                GameObject pickup = (GameObject)Instantiate(freshiePrefab, spawnPointList[i%3], Quaternion.identity);
                pickup.SetActive(false);
                pickup.transform.parent = transform;
                pooledObjects.Add(new ExistingWavePoolItem(pickup, (EnemyTypeInWave) 0, wave.waveState));
            }
            for (int i = 0; i < wave.waveConstant.totalNumberOfISTD; i++)
            {
                //bottomSpawnPoint = new Vector3(Random.Range(bottomSpawn.startX, bottomSpawn.endX), Random.Range(bottomSpawn.startY,bottomSpawn.endY), 0);
                RefreshSpawnList();
                GameObject pickup = (GameObject)Instantiate(istdPrefab, spawnPointList[i%3], Quaternion.identity);
                pickup.SetActive(false);
                pickup.transform.parent = transform;
                pooledObjects.Add(new ExistingWavePoolItem(pickup, (EnemyTypeInWave) 1, wave.waveState));
            }
            for (int i = 0; i < wave.waveConstant.totalNumberOfEPD; i++)
            {
                //bottomSpawnPoint = new Vector3(Random.Range(bottomSpawn.startX, bottomSpawn.endX), Random.Range(bottomSpawn.startY,bottomSpawn.endY), 0);
                RefreshSpawnList();
                GameObject pickup = (GameObject)Instantiate(epdPrefab, spawnPointList[i%3], Quaternion.identity);
                pickup.SetActive(false);
                pickup.transform.parent = transform;
                pooledObjects.Add(new ExistingWavePoolItem(pickup, (EnemyTypeInWave) 2, wave.waveState));
            }
            enemyTimers = new List<float>();
            enemyIntervals = new List<float>();
            enemyTimers.Add(wave.waveConstant.firstFreshie);
            enemyTimers.Add(wave.waveConstant.firstISTD);
            enemyTimers.Add(wave.waveConstant.firstEPD);
            enemyIntervals.Add(wave.waveConstant.freshieInterval);
            enemyIntervals.Add(wave.waveConstant.istdInterval);
            enemyIntervals.Add(wave.waveConstant.epdInterval);
            wave.spawned = true;
            }            
        }
    }

    void RefreshSpawnList()
    {
        spawnPointList = new List<Vector3>();
        spawnPointList.Add(bottomSpawn.getSpawnPoint());
        spawnPointList.Add(leftSpawn.getSpawnPoint());
        spawnPointList.Add(rightSpawn.getSpawnPoint());
    }

    public GameObject GetPooledObject() {
        // return inactive pooled object if it matches the gamestate
        for (var i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].waveState == currentGameState)
            {
                return pooledObjects[i].gameObject;
            }
                
        }
        return null;
    }

    public List<float> GetEnemyTimers()
    {
        if (enemyTimers.Count == 3)
        {
            return enemyTimers;
        }
        Debug.Log("Timer error");
        return null;
    }
    public List<float> GetEnemyIntervals()
    {
        if (enemyIntervals.Count == 3)
        {
            return enemyIntervals;
        }
        Debug.Log("Interval error");
        return null;
    }
    public GameStateVariable GetCurrGameState()
    {
        return currentGameState;
    }
}