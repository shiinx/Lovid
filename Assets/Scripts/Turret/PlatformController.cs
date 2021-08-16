using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool selfDestroy = false ; 
    public float selfDestroyTime = 60f ; 
    public PlayerConstants playerConstants ; 
    private SpriteRenderer spriteRenderer ; 
    private Color color ; 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>() ; 
        color = spriteRenderer.color ; 
        if(selfDestroy){
            StartCoroutine(DestroySelf()) ; 
        }
    }

    IEnumerator DestroySelf(){
        yield return new WaitForSeconds(selfDestroyTime-5f) ; 
        StartCoroutine(Blink()) ; 
        yield return new WaitForSeconds(5f) ; 
        Destroy(this.gameObject) ; 
    }

    IEnumerator Blink(){
        // color.a = 1 ; 
        // spriteRenderer.color = color;
        while(true)
        {
            color.a = 0.2f ; 
            spriteRenderer.color = color;
            yield return new WaitForSeconds(playerConstants.timeBetweenDamageBlink) ; 
            color.a = 1f ; 
            spriteRenderer.color = color;
            yield return new WaitForSeconds(playerConstants.timeBetweenDamageBlink) ; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
