using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ; 
using UnityEngine.Events ; 
public class RestartMenuController : MonoBehaviour
{
    Animator anim ; 
    public UnityEvent onGameReset ;
    public string startingSceneName ; 
    public string nextLevelSceneName ; 
    public BoolVariable isPaused ; 


    

    // Start is called before the first frame update
    void Start()
    {
     anim = GetComponent<Animator>() ; 

    }

    public void quitGame(){
        Debug.Log("resume game called") ; 
        isPaused.Value = false ; 
        Time.timeScale = 1.0f ; 
        if (!string.IsNullOrEmpty(startingSceneName)) SceneManager.LoadScene(startingSceneName);

    }

    public void restartLevel(){
        // call reset game event 
        isPaused.Value = false ; 
        onGameReset.Invoke() ; 
        Time.timeScale = 1.0f ; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextLevel(){
        isPaused.Value = false ;
        Time.timeScale = 1.0f ; 
        if (!string.IsNullOrEmpty(nextLevelSceneName)) SceneManager.LoadScene(nextLevelSceneName);

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
