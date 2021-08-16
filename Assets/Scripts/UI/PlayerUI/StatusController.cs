using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StatusController : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyConstants enemyConstants;
    public GameObject confuse;
    public GameObject weakness;
    public GameObject silence;
    public GameObject turretDebuff;
    void Start() {
        ResetStatus();
    }
    
    public void SilenceResponse() {
        StartCoroutine(Silence());
    }

    public void TurretDebuffResponse() {
        StartCoroutine(Sleep());
    }

    public void WeaknessResponse() {
        StartCoroutine(Weakened());
    }

    public void ConfuseResponse() {
        StartCoroutine(Confused());
    }

    public void ResetStatus() {
        confuse.SetActive(false);
        turretDebuff.SetActive(false);
        silence.SetActive(false);
        weakness.SetActive(false);
    }
    
    private IEnumerator Confused() {
        confuse.SetActive(true);
        yield return new WaitForSecondsRealtime(enemyConstants.confuseDebuffTime);
        confuse.SetActive(false);
    }
    
    private IEnumerator Sleep() {
        turretDebuff.SetActive(true);
        yield return new WaitForSeconds(enemyConstants.turretDebuffTime);
        turretDebuff.SetActive(false);
    }
    
    private IEnumerator Silence() {
        silence.SetActive(true);
        yield return new WaitForSeconds(enemyConstants.silenceDebuffTime);
        silence.SetActive(false);
    }
    
    private IEnumerator Weakened() {
        weakness.SetActive(true);
        yield return new WaitForSeconds(enemyConstants.confuseDebuffTime);
        weakness.SetActive(false);
    }
}
