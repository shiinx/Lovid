using UnityEngine;

// Turret
public class BulletController : MonoBehaviour {
    // Start is called before the first frame update

    public bool penetrate;
    private bool orient = true  ; 
    private Vector3 previousPos ; 
    private void Start() {
        previousPos = this.transform.position ; 
    }

    // Update is called once per frame
    private void Update() { 

        if (orient) {
            orient = false ; 
            var dir = this.transform.position - previousPos; 
            var angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    private void OnBecameInvisible() {
         Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!penetrate) Destroy(gameObject);
    }
}