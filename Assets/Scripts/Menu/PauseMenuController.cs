using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ; 
using UnityEngine.Events ; 
public class PauseMenuController : MonoBehaviour
{
    Animator anim ; 
    public GameObject HelpPanel ; 

    public UnityEvent onGameReset ; 
    public BoolVariable isPaused ; 


    

    // Start is called before the first frame update
    void Start()
    {
     anim = GetComponent<Animator>() ; 
     HelpPanel.SetActive(false) ; 

    }

    public void openHelpPanel(){
        HelpPanel.SetActive(true) ; 
    }

    public void closeHelpPanel(){
        HelpPanel.SetActive(false) ; 
    }

    public void resumeGame(){
        Debug.Log("resume game called") ; 
        isPaused.Value = false ; 
        transform.parent.gameObject.SetActive(false); 
        Time.timeScale = 1.0f ; 

    }

    public void quitGame(){
        // call reset game event 
        isPaused.Value = false ;
        onGameReset.Invoke() ; 
        Time.timeScale = 1.0f ; 
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }


    public void openHelp(){}

    // public void openPausePanel() {
    //     Time.timeScale = 0.0f ; 
    //     //anim.Play("OptTweenAnim_on");
    //     pausePanel.SetActive(true) ; 
    //     playClickSound() ; 
    // }

    void playClickSound(){

    }

    public void playHoverClip(){
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
