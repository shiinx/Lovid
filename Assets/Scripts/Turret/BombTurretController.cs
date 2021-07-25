using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;


// when enemies enter the bomb collision area, they should register themselves into the event 
public class BombTurretController : MonoBehaviour {
    public delegate void gameEvent();

    // Start is called before the first frame update
    public float diffuseTime = 0.5f;
    public float explodeRaidus;

    public List<EnemyType> hittableEnemeies ; 
    private bool active;
    private AudioSource bombAudioSource;

    private void Start() {
        bombAudioSource = GetComponent<AudioSource>();
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

    private void OnTriggerEnter2D(Collider2D other) {
        if (enemyDetected(other))
            if (!active) {
                active = true;
                GetComponent<CircleCollider2D>().radius = explodeRaidus;

                StartCoroutine(diffuse());
            }
    }

    private IEnumerator diffuse() {
        yield return new WaitForSeconds(diffuseTime);
        Destroy(gameObject);
        // exploding
    }
}