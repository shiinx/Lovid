using System.Collections;
using UnityEngine;


// when enemies enter the bomb collision area, they should register themselves into the event 
public class BombTurretController : MonoBehaviour {
    public delegate void gameEvent();

    // Start is called before the first frame update
    public float diffuseTime = 1.0f;
    public float explodeRaidus;

    private bool active;
    private AudioSource bombAudioSource;

    private void Start() {
        bombAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update() { }

    // void OnTriggerEnter2D(Collider2D other){
    //     if (other.gameObject.tag =="Enemy"){
    //         if (!active) {
    //             active = true ; 
    //             // increase collider radius
    //             //GetComponent<CircleCollider2D>().radius = Time.time * .01f;
    //             GetComponent<CircleCollider2D>().radius = explodeRaidus ; 
    //             // add object

    //         }
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("collision detected");
        if (other.gameObject.tag == "Enemy")
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