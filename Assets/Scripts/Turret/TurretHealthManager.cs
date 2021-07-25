using UnityEngine;
using System;
using System.Collections.Generic;
public enum TurretType {
    attack = 0,
    defence = 1
}

public class TurretHealthManager : MonoBehaviour {
    // Start is called before the first frame update

    public EnemyConstants enemyConstants;
    public BossConstants bossConstants;
    public TurretConstants turretConstants;
    public TurretType turretType;
    private float currentHP;
    private float maxHP;

    private void Start() {
        if (turretType == TurretType.attack)
            currentHP = turretConstants.attackTurretHealth;
        else if (turretType == TurretType.defence) currentHP = turretConstants.defenseTurretHealth;
        maxHP = currentHP;
    }

    // Update is called once per frame
    private void Update() { }

    // implementing long range damaage 

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "BossBullet") currentHP -= bossConstants.rangeDamage; // yes, no dividing by number of frames 

        if (currentHP <= 0) killself();
    }


    // implementing touch damage
    private void OnTriggerStay2D(Collider2D other) {
        // needs to be updated based on enemy type 
        // if (other.gameObject.tag == "Enemy") {
        //     // check for enemy type

        //     Debug.Log("Collision ");
        //     // take damage 
        //     currentHP -= enemyConstants.EnemyCP / 50;
        //     Debug.Log(currentHP);
        //     // check if health reached zero 

        //     // if health is zero, kill self 
        // }

        // freshie , ISTD , EPD, boss, bossbullet, 

        if (other.gameObject.tag == "Freshie") currentHP -= enemyConstants.freshieTouchDamage / 50;

        if (other.gameObject.tag == "ISTD") currentHP -= enemyConstants.istdTouchDamage / 50;

        if (other.gameObject.tag == "EPD") currentHP -= enemyConstants.epdTouchDamage / 50;

        if (other.gameObject.tag == "Boss") currentHP -= bossConstants.touchDamage / 50;

        Debug.Log(currentHP) ; 
        Debug.Log("hello") ; 
        if (currentHP <= 0) killself();
    }


    private void killself() {
        Destroy(transform.parent.gameObject);
    }
}