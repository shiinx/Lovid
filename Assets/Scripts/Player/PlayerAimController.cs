using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    private Transform _aimTransform;
    private Camera _camera;
    public Transform gunMuzzle;
    public PlayerVariable playerVariable;
    public GameObject bullet;
    private Animator _gunAnimator;
    private AudioSource _gunAudio;
    void Start() {
        _aimTransform = GetComponent<Transform>();
        _gunAudio = GetComponent<AudioSource>();
        _gunAnimator = GetComponent<Animator>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        var playerPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDirection = (mousePosition - playerPosition).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        var aimTransformLocalScale = _aimTransform.localScale;
        if (angle > 90 || angle < -90) {
            aimTransformLocalScale.x = -1f;
            aimTransformLocalScale.y = -1f;
        } else {
            aimTransformLocalScale.x = 1f;
            aimTransformLocalScale.y = 1f;
        }
        _aimTransform.eulerAngles = new Vector3(0, 0, angle);
        _aimTransform.localScale = aimTransformLocalScale;
    }
    
    public void Shoot() {
        _gunAudio.PlayOneShot(_gunAudio.clip);
        _gunAnimator.SetTrigger("onShoot");
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        var bulletPosition = new Vector2(gunMuzzle.position.x, gunMuzzle.position.y);
        var aimDirection = (mousePosition - bulletPosition).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        playerVariable.PlayerBulletDirection = mousePosition - new Vector2(bulletPosition.x, 
        bulletPosition.y);
        var bulletAngle = Quaternion.Euler(new Vector3(0, 0, angle)) ;
        Instantiate(bullet, bulletPosition, bulletAngle);
    }
}
