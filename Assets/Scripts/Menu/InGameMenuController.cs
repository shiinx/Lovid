using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameMenuController : MonoBehaviour
{
    private Animator anim ; 
    public GameObject pausePanel ; 
    public UnityEvent onMenuClicked;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>() ; 
    }

    public void openPausePanel() {
        onMenuClicked.Invoke() ; 
        Time.timeScale = 0.0f ; 
        //  anim.Play("OptTweenAnim_on");
        pausePanel.SetActive(true) ; 
        playClickSound() ; 
    }

    public void speedUP(){
        if (Time.timeScale == 1.0f) { 
            Time.timeScale = 2.0f ; 
        } else {
            Time.timeScale = 1.0f ; 
        }
        Debug.Log(Time.timeScale) ; 
        playClickSound() ; 
    }

    void playClickSound(){

    }

    public void playHoverClip(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
