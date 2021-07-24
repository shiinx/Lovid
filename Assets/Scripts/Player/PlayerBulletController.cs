using UnityEngine;

public class PlayerBulletController : MonoBehaviour {
    private Rigidbody2D _bulletBody;

    public PlayerConstants playerConstants;

    public Vector2Variable direction;
    // Start is called before the first frame update
    void Start() {
        _bulletBody = GetComponent<Rigidbody2D>();
        _bulletBody.AddForce(direction.Value * playerConstants.bulletForce / direction.Value.magnitude, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible() {
        Debug.Log("to destroy");
        Destroy(gameObject);
    }
}
