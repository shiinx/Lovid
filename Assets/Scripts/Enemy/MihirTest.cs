using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events ; 

[System.Serializable]
public class EnemySpawnObject{
    public GameObject enemy ; 
    public Location location ;
    public float startTiming ; 
    public float delay;
    public float repeat ; 
}

[System.Serializable]
public class WaveObjects{
    public bool include = true ; 
    public List<EnemySpawnObject> waveEnemies ; 
    
}

[System.Serializable]
public class LocationMapping{
    public Location locationName; 
    public GameObject location  ; 
}

public enum Location{
        topLeft, 
        topRight, 
        bottomLeft, 
        bottomRight, 
        upperMiddleLeft, 
        upperMiddleRight, 
        lowerMiddleLeft, 
        lowerMiddleRight
    }

public enum Enemies{
    Freshie, 
    ISTD, 
    EPD
}



public class MihirTest : MonoBehaviour
{
    
    public List<LocationMapping> locationMapping ;  
    public List<WaveObjects> enemySpawnObjectsWaves ; 
    public GameStateVariable currentGameState ; 
    public UnityEvent UIChangeEvent ; 
    public IntVariable waveNumber ; 
    public IntVariable enemyLeft; 
    public IntVariable numberOfWaves ; 
    
    private bool firstWave = true ; 
    // public int numberOfBossWaves ; 
    // public IntVariable bossPhase ; 
    // public IntVariable numberOfBossWavesIntVariable ; 
    
    public float TimeBetweenWaves = 10f ; 
    void Start()
    {   
        //numberOfBossWavesIntVariable.Value = numberOfBossWaves ; 
        numberOfWaves.Value = enemySpawnObjectsWaves.Count ; 
        //bossPhase.Value = 0 ; 
        UIChangeEvent.Invoke() ; 
    }

    Transform getLocation(Location location){
        for (int i = 0; i < locationMapping.Count; i++)
        {
         if(location==locationMapping[i].locationName){
             return locationMapping[i].location.transform; 
         }   
        }

        return null ;
    }
    IEnumerator spawnEnemy(EnemySpawnObject enemySpawnObject){
        yield return new WaitForSeconds(enemySpawnObject.startTiming) ; 
        for (int j = 0; j < enemySpawnObject.repeat; j++)
        {
            var enemy = Instantiate(enemySpawnObject.enemy, getLocation(enemySpawnObject.location)) ;
            yield return new WaitForSeconds(enemySpawnObject.delay) ;

        }    
    }

    public void stateChangeResponse(){

        if (waveNumber.Value==0){
            // build state
        } else if (waveNumber.Value<0){
            // game over state

        } else if (waveNumber.Value > enemySpawnObjectsWaves.Count){
            // 
        }
        else {
            //wave state
            var wave = enemySpawnObjectsWaves[waveNumber.Value-1] ; 
            if (enemyLeft.Value!=0) Debug.Log("Error in the state machine, enemyLeft is not zero") ; 
            enemyLeft.Value = 0  ;
            for (int i = 0; i < wave.waveEnemies.Count; i++){
                enemyLeft.Value += (int) wave.waveEnemies[i].repeat ; 
            }
            UIChangeEvent.Invoke() ; 

            StartCoroutine(startNextWave(wave)) ; 
 

        }
    }

    IEnumerator startNextWave(WaveObjects wave){
        if (!firstWave){
            yield return new WaitForSeconds(TimeBetweenWaves) ; 
        }
        
        // var wave = enemySpawnObjectsWaves[waveNumber.Value-1] ; 
        // if (enemyLeft.Value!=0) Debug.Log("Error in the state machine, enemyLeft is not zero") ; 
        // enemyLeft.Value = 0  ;


        // for (int i = 0; i < wave.waveEnemies.Count; i++){
        //     enemyLeft.Value += (int) wave.waveEnemies[i].repeat ; 
        // }
        // UIChangeEvent.Invoke() ; 
        // for (int i = 0 ; i < wave.waveEnemies.Count; i++){
        //     StartCoroutine(spawnEnemy(wave.waveEnemies[i])) ; 
        // } 
        firstWave = false ; 
        if (wave.include){
            for (int i = 0 ; i < wave.waveEnemies.Count; i++){
                StartCoroutine(spawnEnemy(wave.waveEnemies[i])) ; 
            }
        } else {
            waveNumber.Value += 1;
            stateChangeResponse();
        }



    }
    // Update is called once per frame
    void Update()
    {
        

        
    }


}
