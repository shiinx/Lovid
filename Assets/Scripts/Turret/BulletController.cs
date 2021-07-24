using UnityEngine;

// Turret
public class BulletController : MonoBehaviour {
    // Start is called before the first frame update

    public bool penetrate;

    private void Start() { }

    // Update is called once per frame
    private void Update() { }

    private void OnBecameInvisible() {
        Debug.Log("to destroy");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D() {
        if (!penetrate) Destroy(gameObject);
    }
}