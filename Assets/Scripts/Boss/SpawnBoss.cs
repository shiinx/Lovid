using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameStateVariable currentGameState ; 
    public GameObject BossPhase1;
    public GameObject BossPhase2;

    public float x ; 
    public float y ; 
    public float z ; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StateChangedResponse(){
        if(currentGameState.Value == GameConstants.GameState.Level1Boss1){
            Instantiate(BossPhase1, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if(currentGameState.Value == GameConstants.GameState.Level1Boss2){
            Instantiate(BossPhase2, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
