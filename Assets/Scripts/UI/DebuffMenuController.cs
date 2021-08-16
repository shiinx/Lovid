using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuffMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject imageObject ; 
    public GameObject titleObject ; 
    public GameObject bodyObject ; 
    public EnemyConstants enemyConstants ; 
    private Text titleText; 
    private Text bodyText; 
    private Image iconImage ; 
    private bool onDebuff = false ; 

    void Start()
    {
        titleText = titleObject.GetComponent<Text>() ;
        bodyText = bodyObject.GetComponent<Text>() ; 
        iconImage = imageObject.GetComponent<Image>() ; 

        titleText.text = "Debuff" ; 
        bodyText.text = "None : 0 secs" ; 
    }

    public void onShootingDebuffResponse(){
        if(!onDebuff){
            StartCoroutine(activateDebuff("Shooting", "is disabled", enemyConstants.shootingDebuffTime)) ; 
        }
    }

    public void onTurretDebuffResponse(){
        if(!onDebuff){
            StartCoroutine(activateDebuff("All Turrets", "are disabled", enemyConstants.turretDebuffTime)) ; 
        }
    }

    public void onSilenceDebuffResponse(){
        if(!onDebuff){
            StartCoroutine(activateDebuff("Building Turrets", "is disabled", enemyConstants.silenceDebuffTime)) ; 
        }
    }

    public void onMirrorDebuffResponse(){
        if(!onDebuff){
            StartCoroutine(activateDebuff("Controls are", "MIRRORED", enemyConstants.confuseDebuffTime)) ; 
        }
    }

    IEnumerator activateDebuff(string title, string text, float time){
        onDebuff = true ; 
        titleText.text = title ; 
    
        
        for (int i = (int) time; i > 0; i--)
        {
            bodyText.text = text + " : " + i + " secs" ;
            yield return new WaitForSeconds(1f) ; 

        }
        onDebuff = false ; 
        titleText.text = "Debuff" ; 
        bodyText.text = "None : 0 secs" ;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
