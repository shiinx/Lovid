using UnityEngine;

public class PlayerBulletController : MonoBehaviour {
    private Rigidbody2D _bulletBody;
    public PlayerConstants playerConstants;
    public PlayerVariable playerVariable;
    private Vector3 _startPos ; 
    private Vector3 _dir; 
    // Start is called before the first frame update
    void Start() {
        _bulletBody = GetComponent<Rigidbody2D>();
        _bulletBody.AddForce(playerVariable.PlayerBulletDirection * playerConstants.bulletForce / playerVariable.PlayerBulletDirection.magnitude, ForceMode2D.Impulse);
        _startPos = transform.position; 
    }
    
    void Update()
    {
        if ((transform.position-_startPos).magnitude >= playerVariable.PlayerShootDistance){
            Destroy(gameObject); 
        }
    }

    private void OnBecameInvisible() {
        // Debug.Log("to destroy");
        Destroy(gameObject);
    }
}
