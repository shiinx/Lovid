using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events ; 

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public BoolVariable tutorialOver ; 
    public List<GameObject> tips ; 
    public BoolVariable isGamePaused ; 
    public UnityEvent onTutorialEnd ; 
    public GameObject flag ; 
    public GameObject enemySpawner ; 
    public bool requireTutorial = true ; 
    private int index ; 
    private bool next = false ; 
    private bool onTutorialEndTriggered = false ; 

    void pauseGame(){
        isGamePaused.Value = true ; 
        Time.timeScale = 0.0f ; 
    }

    void unpauseGame(){
        isGamePaused.Value = false ; 
        Time.timeScale = 1.0f ; 
    }
    void Start()
    {   
        isGamePaused.Value = true ; 
        Time.timeScale = 0.0f ; 
        if (requireTutorial&&!tutorialOver.Value){
            index = 0 ; 
            tips[index].SetActive(true) ; 
        } else {
            index = tips.Count ; 
        }

    }
    // remove direct links 
    // Update is called once per frame
    void Update()
    {
        // flesh out for each individual index 
        // need to add for flag movement, currency system and story

        switch (index)
        { 
            case 0 : 
                if (Input.GetKeyUp(KeyCode.Tab)){
                    tips[index].SetActive(false) ; 
                    index += 1 ; 
                    if (index==tips.Count){
                        index = 100 ; 
                    } else {
                        tips[index].SetActive(true) ; 
                    }
                    
                }
                //index = 6 ; // remove this , uncomment up and in start

                
                break ; 

            case 1 : 
                unpauseGame() ; 
                if (Input.GetKeyUp(KeyCode.Alpha1)) {
                    pauseGame() ; 
                    tips[index].SetActive(false) ; 
                    index += 1 ; 
                    if (index==tips.Count){
                        index = 100 ; 
                    } else {
                        tips[index].SetActive(true) ; 
                    } 
                }
                break ; 

            case 2 : 
            // equipment
                if (Input.GetKeyUp(KeyCode.Tab)){
                    pauseGame() ; 
                    tips[index].SetActive(false) ; 
                    index += 1  ; 
                    if (index==tips.Count){
                        index = 100 ; 
                    } else {
                        tips[index].SetActive(true) ; 
                    } 
                }
                break ; 
            
            case 3 : 
            // shooting 
                unpauseGame() ; 
                if (Input.GetMouseButtonUp(0)){
                    pauseGame() ; 
                    tips[index].SetActive(false) ; 
                    index += 1  ; 
                    if (index==tips.Count){
                        index = 100 ; 
                    } else {
                        tips[index].SetActive(true) ; 
                    }
                    
                }
                break ; 
            case 4 : 
            //platform 
                unpauseGame() ; 
                if (Input.GetKeyDown(KeyCode.Alpha2)) {
                        next = true ; 
                    }
                    if (Input.GetMouseButtonUp(0) && next){
                        pauseGame() ; 
                        tips[index].SetActive(false) ; 
                        index += 1  ; 
                        // show rest of turrets
                        if (index==tips.Count){
                            index = 100 ; 
                        } else {
                            tips[index].SetActive(true) ;  
                        }

                    }
                break ; 

            case 5 : 
                if (Input.GetKeyUp(KeyCode.Tab)){
                    pauseGame() ; 
                    tips[index].SetActive(false) ; 
                    index += 1  ; 
                    if (index==tips.Count){
                        index = 100 ; 
                    } else {
                        tips[index].SetActive(true) ; 
                    } 
                }
                break ; 

            case 6 : 
            // platform 
                unpauseGame() ; 
                if (Input.GetKeyUp(KeyCode.Tab)){
                    pauseGame() ; 
                    tips[index].SetActive(false) ; 
                    index += 1  ; 
                    // show rest of turrets
                    if (index==tips.Count){
                        index = 100 ; 
                    } else {
                        for (int i = index; i < tips.Count; i++)
                        {
                            tips[i].SetActive(true) ; 
                        }   
                    }

                }
                break ; 
            
            case 7 : 
            // turrets
                if (Input.GetKeyUp(KeyCode.Tab)){
                    for (int i = index; i < tips.Count; i++)
                    {
                        tips[i].SetActive(false) ; 
                    } 
                    index += 1  ; 
                }
                break ; 
            
            default:
            // end 
                if(!onTutorialEndTriggered){
                    onTutorialEndTriggered = true ; 
                    Debug.Log("here") ; 
                    unpauseGame() ; 
                    flag.SetActive(true) ; 
                    enemySpawner.SetActive(true) ; 
                    onTutorialEnd.Invoke() ; 
                    tutorialOver.Value = true ; 
                    this.gameObject.SetActive(false) ; 
                    
                }
                break ; 
        }
    }

    IEnumerator disableSelf(){
        yield return new WaitForSeconds(1f) ; 
        this.transform.parent.gameObject.SetActive(false) ; 

    }
}
