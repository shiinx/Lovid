using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

// [System.Serializable]
    public enum EnemyType{
            Freshie, 
            ISTD, 
            EPD, 
            Boss
        }

public class AttackTurretController : MonoBehaviour {

    public GameObject bullet;

    public List<EnemyType> hittableEnemeies ; 

    public TurretConstants turretConstants ; 

    public float bulletForce;
    private AudioSource AttackTurretAudioSource;
    private float CP;
    private float HP;
    private bool active = true ; 

    // Start is called before the first frame update
    private void Start() {
        AttackTurretAudioSource = GetComponent<AudioSource>();
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
        Debug.Log("Collision detected");
        print(other.gameObject.tag);
        if (enemyDetected(other) && isActive()) {
            
            var click = other.transform.position;
            var bulletInstance = Instantiate(bullet);
            bulletInstance.transform.position = transform.position;
            Vector2 direction = click - bulletInstance.transform.position;
            var bulletBody = bulletInstance.GetComponent<Rigidbody2D>();
            bulletBody.AddForce(direction * bulletForce, ForceMode2D.Force);
            // add in particle effects 
            // add in sound
        }
    }
}