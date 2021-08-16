using UnityEngine;

// boss
public class HomingBullet : MonoBehaviour {
    public float speed = 5;
    public float time = 3 ; 
    private Transform player;

    // Start is called before the first frame update
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    private void Update() {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        //moveBullet() ; 
    }
}