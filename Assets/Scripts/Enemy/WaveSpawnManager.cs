using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnManager : MonoBehaviour
{
    private List<float> enemyTimers;
    private List<float> enemyIntervals;
    private GameStateVariable previousState;
    private GameStateVariable currGameState;
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        previousState = WavePooler.SharedInstance.GetCurrGameState();
    }

    // Update is called once per frame
    void Update()
    {
        currGameState = WavePooler.SharedInstance.GetCurrGameState();
        if (currGameState != previousState)
        {
            GetTimersAndIntervals();
        }
        if (enemyIntervals != null && enemyTimers != null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (enemyTimers[i] <= 0)
                {
                    spawnFromPooler();
                    enemyTimers[i] = enemyIntervals[i];
                }
                enemyTimers[i]-= Time.deltaTime;
            }
        }
    }

    void  GetTimersAndIntervals()
    {
        enemyTimers = WavePooler.SharedInstance.GetEnemyTimers();
        enemyIntervals = WavePooler.SharedInstance.GetEnemyIntervals();
    }

    void spawnFromPooler()
    {
        GameObject enemy = WavePooler.SharedInstance.GetPooledObject();
        if (enemy != null)
        {
            //can choose object possition here as well osadfdjnagfabn wy i go  make that stupid spawnpointlist jn
            //
            enemy.SetActive(true);
        }
        else
        {
            Debug.Log("No more enemies for  the wave, or wavepooler/spawnmanager got prob, dun ask me for specifics, too much effort is spent on this");
        }
    }
}
