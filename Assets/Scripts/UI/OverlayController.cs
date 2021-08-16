using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    // Start is called before the first frame update
    private Color color ; 
    private SpriteRenderer spriteRenderer ; 
    public float lowAlpha = 0.3f  ; 
    public  float highAlpha = 0.5f ; 
    public float step = 0.1f ; 
    private float alpha  ; 
    public float time = 1f ; 
    private bool increase = true ; 
    void Start()
    {
        color = this.gameObject.GetComponent<SpriteRenderer>().color;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>() ; 
        alpha = lowAlpha ; 
        color.a = lowAlpha ; 
        spriteRenderer.color = color ; 
        this.gameObject.SetActive(false) ; 

    }

    // Update is called once per frame
    void Update()
    {
        if(increase){
            increase = alpha > highAlpha ? false : true ; 
        }else {
            increase = alpha < lowAlpha ? true : false ; 
        }

        alpha += increase ? step/10 : -step/10 ;   
        color.a = alpha ; 
        spriteRenderer.color = color;
    }


}
