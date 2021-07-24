using UnityEngine;

public class AttackTurretController : MonoBehaviour {
    public GameObject bullet;

    public float bulletForce;
    private AudioSource AttackTurretAudioSource;
    private float CP;
    private float HP;

    // Start is called before the first frame update
    private void Start() {
        AttackTurretAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update() { }


    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log("Collision detected");
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy") {
            print("in");
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