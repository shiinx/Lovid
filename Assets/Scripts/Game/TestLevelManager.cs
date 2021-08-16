using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events ; 
using UnityEngine.SceneManagement;
public class TestLevelManager : MonoBehaviour
{
    public int level ; 
    public IntVariable currentGameLevel ; 
    public GameStateVariable currentGameState ; 
    public IntVariable numberOfBossWaves  ; 
    public UnityEvent onStateChanged ; 
    public UnityEvent UIChangeEvent ; 
    public string startingSceneName ; 
    public string nextLevelSceneName ;
    public GameObject restartMenu ; 
    public GameObject successMenu ; 
    public BoolVariable isPaused ; 
    public IntVariable enemyLeft;
    public IntVariable waveNumber ; 
    public IntVariable numberOfWaves ; 
    public GameObject bossHealthBar ; 
    // public IntVariable bossPhase ; 


    // Start is called before the first frame update
    void Start()
    {   
        enemyLeft.Value = 0 ; 
        isPaused.Value = false ; 
        Time.timeScale = 1.0f ; 
        waveNumber.Value = 1 ; 
        currentGameLevel.Value = level ; 
        bossHealthBar.SetActive(false) ; 
        // Debug.Log("here invoking") ;
        onStateChanged.Invoke() ; 
        UIChangeEvent.Invoke() ;  
        //bossPhase.Value = 0 ; 
        
 
    }

    public void onBossAppearResponse(){
        bossHealthBar.SetActive(true) ; 
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDeathResponse(){
        // player doesn't call event by itself, instead calls this
        isPaused.Value = true ; 
        waveNumber.Value = -1 ;
        Debug.Log("here invoking 2") ; 
        onStateChanged.Invoke() ; 
        Time.timeScale = 0.0f ; 
        restartMenu.SetActive(true) ; 
    }

    public void FlagDeathResponse(){
        isPaused.Value = true ; 
        waveNumber.Value = -1 ;
        Debug.Log("here invoking 3 ") ; 
        onStateChanged.Invoke() ; 
        Time.timeScale = 0.0f ; 
        restartMenu.SetActive(true) ; 
    }


    // listen to onEnemyDeath event
    public void enemyDeathResponse(){
        enemyLeft.Value -= 1 ; 
        if (enemyLeft.Value<= 0){
            if (true)//(waveNumber.Value<=numberOfWaves.Value-numberOfBossWaves.Value)
                {
                    waveNumber.Value += 1 ; 
                    enemyLeft.Value = 0 ; 
                    onStateChanged.Invoke() ; 
                    
                }
            else {
                //bossPhase.Value = 1 ; 
            }
        }

        if (waveNumber.Value > numberOfWaves.Value){
            isPaused.Value = true ; 
            Time.timeScale = 0f ; 
            successMenu.SetActive(true) ; 
        }
        UIChangeEvent.Invoke() ; 
    }
    // change to numbers with float 
    public void bossEnemyDeathResponse(){
        bossHealthBar.SetActive(false) ; 
        enemyLeft.Value -= 1 ; 
        if (enemyLeft.Value<= 0){
            waveNumber.Value += 1 ; 
            enemyLeft.Value = 0 ;  
            onStateChanged.Invoke() ; 
        }

        if (waveNumber.Value > numberOfWaves.Value){
            isPaused.Value = true ; 
            Time.timeScale = 0f ; 
            successMenu.SetActive(true) ; 
        }
        UIChangeEvent.Invoke() ; 
    }


    public void resetResponse(){
        currentGameState.Value = GameConstants.GameState.Reset ; 
        onStateChanged.Invoke() ; 
    }

    public void quitResponse(){ 
         if (!string.IsNullOrEmpty(startingSceneName)) SceneManager.LoadScene(startingSceneName);
    }

}