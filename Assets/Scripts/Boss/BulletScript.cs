using UnityEngine;

//boss
public class BulletScript : MonoBehaviour {
    public float speed;
    private Rigidbody2D bulletRB;

    private GameObject target;

    // Start is called before the first frame update
    private void Start() {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = -(target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    private void Update() {
    }
}