using UnityEngine;

// boss
public class HomingBullet : MonoBehaviour {
    public float speed = 5;

    private Transform player;

    // Start is called before the first frame update
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}