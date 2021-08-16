using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events ; 

[System.Serializable]
public enum InputEnum{
    Tab  , 
    Click  , 
    Alpha1 , 
    Alpha2 
}

[System.Serializable]
public class TutorialObject{
    public GameObject Tooltip ; 
    public InputEnum input ; 
}

public class UpdatedTutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public BoolVariable tutorialOver ; 
    public List<TutorialObject> tutorialObjects ; 
    public BoolVariable isGamePaused ; 
    public UnityEvent onTutorialEnd ; 
    public GameObject flag ; 
    public GameObject enemySpawner ; 
    public bool requireTutorial = true ; 
    private int index = 0; 
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

    KeyCode inputMapping(InputEnum inputEnum){
        switch (inputEnum) {

            case InputEnum.Tab : 
                return KeyCode.Tab ; 

            case InputEnum.Alpha1 : 
                return KeyCode.Alpha1 ; 

            case InputEnum.Alpha2 : 
                return KeyCode.Alpha2 ; 

            case InputEnum.Click : 
                return KeyCode.None; 
                
            default : 
                return KeyCode.None ; 
        }
            
    }
    void Start()
    {   
        if(!requireTutorial || tutorialOver.Value){
            tutorialOver.Value = true ; 
            index = tutorialObjects.Count ; 
        }else{
            tutorialObjects[index].Tooltip.SetActive(true) ; 
        }
    }
    // remove direct links 
    // Update is called once per frame
    void Update()
    {
        unpauseGame() ; 
        if (index==tutorialObjects.Count){
            if(!onTutorialEndTriggered){
                onTutorialEndTriggered = true ; 
                unpauseGame() ; 
                flag.SetActive(true) ; 
                enemySpawner.SetActive(true) ; 
                onTutorialEnd.Invoke() ; 
                tutorialOver.Value = true ; 
                this.gameObject.SetActive(false) ; 
            }

        }else if(tutorialObjects[index].input == InputEnum.Click){
            if (Input.GetMouseButtonUp(0)){
                pauseGame() ; 
                tutorialObjects[index].Tooltip.SetActive(false) ; 
                index += 1 ; 
                if (index==tutorialObjects.Count){
                    // go to end state
                }else{
                    tutorialObjects[index].Tooltip.SetActive(true) ; 
                }
            }

        }else {
            if (Input.GetKeyUp(inputMapping(tutorialObjects[index].input))){
                pauseGame() ; 
                tutorialObjects[index].Tooltip.SetActive(false) ; 
                index += 1 ; 
                if (index==tutorialObjects.Count){
                    // go to end state
                }else{
                    tutorialObjects[index].Tooltip.SetActive(true) ; 
                }
            }
        }
    }

    IEnumerator disableSelf(){
        yield return new WaitForSeconds(1f) ; 
        this.transform.parent.gameObject.SetActive(false) ; 

    }
}