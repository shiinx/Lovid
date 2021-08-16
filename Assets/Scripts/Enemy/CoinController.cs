using UnityEngine;
using System.Collections;


// Turret
public class CoinController : MonoBehaviour {
    // Start is called before the first frame update
    public float lifeTime = 10f ; 
    private void Start() {
        StartCoroutine(KillSelf(lifeTime)) ; 
    }

    // Update is called once per frame
    private void Update() { }

    
    IEnumerator KillSelf(float time){
        yield return new WaitForSeconds(time) ;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            StartCoroutine(KillSelf(0.1f)) ; 
        }

    }
}