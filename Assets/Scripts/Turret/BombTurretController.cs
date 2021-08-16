using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;


// when enemies enter the bomb collision area, they should register themselves into the event 
public class BombTurretController : MonoBehaviour {
    
    public EnemyConstants enemyConstants ; 
    public float diffuseTime = 0.5f;
    public float explodeRaidus;
    public GameObject explosionParticleSystem ; 
    public List<EnemyType> hittableEnemeies ; 
    private bool active;
    private AudioSource bombAudioSource;
    private SpriteRenderer bombSprite ; 
    private bool turretDebuff  = false ;
    public PlayerVariable playerVariable;
    private CircleCollider2D _collider2D;

    private void Start() {
        bombAudioSource = GetComponent<AudioSource>();
        bombSprite = GetComponent<SpriteRenderer>() ;
        _collider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    private void Update() {
    }

    private void FixedUpdate() {
        _collider2D.enabled = !playerVariable.PlayerTurretDebuffed;
    }

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
                explosionParticleSystem.SetActive(true) ; 
                bombAudioSource.Play() ; 
                active = true;
                GetComponent<CircleCollider2D>().radius = explodeRaidus;
                StartCoroutine(diffuse());
            }
    }

    private IEnumerator diffuse() {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}