using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

// [System.Serializable]
    public enum EnemyType{
            Freshie, 
            FreshieVariant, 
            ISTD,
            ISTDVariant,
            EPD, 
            EPDVariant , 
            Boss
        }

public class AttackTurretController : MonoBehaviour {

    public GameObject bullet;
    public List<EnemyType> hittableEnemeies ; 
    public TurretConstants turretConstants ; 
    public PlayerConstants playerConstants ; 
    public EnemyConstants enemyConstants ; 
    public GameObject aimNozzle ; 
    public GameObject blinkingBody ; 
    private Vector3 aimNozzleDirecton ; 
    public float bulletForce;
    private AudioSource AttackTurretAudioSource;
    private float CP;
    private float HP;
    private bool active = true ;
    public bool selfDestroy = false  ; 
    public float selfDestroyTime = 60f; 
    public PlayerVariable playerVariable;

    

    
    // Start is called before the first frame update
    private void Start() {
        AttackTurretAudioSource = GetComponent<AudioSource>();
        aimNozzleDirecton = new Vector3(1, 0 , 0) ; 
        if (selfDestroy){
            StartCoroutine(DestroySelf()) ; 
        }   
    }

    IEnumerator DestroySelf(){
        yield return new WaitForSeconds(selfDestroyTime-5f) ; 
        StartCoroutine(Blink()) ; 
        yield return new WaitForSeconds(5f) ; 
        Destroy(this.transform.parent.gameObject) ; 
    }

    IEnumerator Blink(){
        // color.a = 1 ; 
        // spriteRenderer.color = color;
        while(true)
        {
            blinkingBody.SetActive(false) ; 
            yield return new WaitForSeconds(playerConstants.timeBetweenDamageBlink) ; 
            blinkingBody.SetActive(true) ; 
            yield return new WaitForSeconds(playerConstants.timeBetweenDamageBlink) ; 
        }
    }

    // Update is called once per frame
    private void Update() { }

    private bool enemyDetected(Collider2D other){
        
        for (int i = 0 ; i < hittableEnemeies.Count; i++) {
            if (other.gameObject.tag == hittableEnemeies[i].ToString()){
                
                return true ; 
            }
        }

        return false ; 
    }

    private IEnumerator sleep() {
        yield return new WaitForSeconds(turretConstants.attackTurretDelay);
        active = true ; 
    }

    private bool isActive(){
        if (active) {
            active = false ; 
            StartCoroutine(sleep()) ; 
            return true ; 
        } 
        return false ; 
    }



    private void OnTriggerStay2D(Collider2D other) {
        if (enemyDetected(other) && isActive() && !playerVariable.PlayerTurretDebuffed) {
            var click = other.transform.position;
            var bulletInstance = Instantiate(bullet);
            bulletInstance.transform.position = this.gameObject.transform.position ; 
            Vector3 direction = click - bulletInstance.transform.position;
            var bulletBody = bulletInstance.GetComponent<Rigidbody2D>();

            // change aim nozzle direction 
            float angleChangeNozzle = Vector3.Angle(aimNozzleDirecton, direction) ; 
            aimNozzle.transform.Rotate(0,0,-angleChangeNozzle) ; 
            aimNozzleDirecton = direction/direction.magnitude ; 
            bulletBody.AddForce(direction * bulletForce, ForceMode2D.Force);
            AttackTurretAudioSource.Play() ; 

        }

   }
}