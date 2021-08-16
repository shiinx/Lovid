using UnityEngine;

public class MovementController : MonoBehaviour {
    public ContactFilter2D contactFilter;
    public PlayerConstants playerConstants;
    public PlayerVariable playerVariable;
    private bool _faceRightState = true;
    private Animator _playerAnimator;
    private Rigidbody2D _playerBody;
    private Transform _playerTransform;
    private Vector2 _movement;

    private Camera _camera;
    private bool _shouldJump;

    private bool IsGrounded => _playerBody.IsTouching(contactFilter);

    // Start is called before the first frame update
    private void Start() {
        Application.targetFrameRate = 30;
        _playerBody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerTransform = GetComponent<Transform>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void Update() {
        _playerAnimator.SetBool("onGround", IsGrounded);
        if (Input.GetKeyDown(KeyCode.Space)) _shouldJump = true;

        
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        
        
        // change sprite looking direction based on mouse location
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        var position = _playerTransform.position;
        var localScale = _playerTransform.localScale;
        if (mousePosition.x < position.x && _faceRightState) {
            _faceRightState = false;
            localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            _playerTransform.localScale = localScale;
        } else if (mousePosition.x > position.x && !_faceRightState) {
            _faceRightState = true;
            localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            _playerTransform.localScale = localScale;
        }
    }

    private void FixedUpdate() {
        MovePlayer(_movement);
        if (_shouldJump && IsGrounded) Jump();

        _shouldJump = false;
        ModifyPhysics(_movement);
    }

    private void MovePlayer(Vector2 movement) {
        // move by add force
        _playerBody.AddForce(movement * (playerConstants.speed * playerVariable.PlayerConfused));
        // set max speed 
        if (Mathf.Abs(_playerBody.velocity.x) > playerConstants.maxSpeed)
            _playerBody.velocity = new Vector2(Mathf.Sign(_playerBody.velocity.x) * playerConstants.maxSpeed,
                _playerBody.velocity.y);

        _playerAnimator.SetFloat("xSpeed", Mathf.Abs(movement.x));
        _playerAnimator.SetFloat("ySpeed", _playerBody.velocity.y);
    }

    private void Jump() {
        _playerBody.AddForce(Vector2.up * playerConstants.upSpeed, ForceMode2D.Impulse);
    }

    private void ModifyPhysics(Vector2 movement) {
        var velocity = _playerBody.velocity;
        var changingDirections = movement.x > 0 && velocity.x < 0 || movement.x < 0 && velocity.x > 0;

        if (IsGrounded) {
            if (Mathf.Abs(movement.x) < 0.4f || changingDirections)
                _playerBody.drag = playerConstants.linearDrag * 0.15f;
            else
                _playerBody.drag = 0f;
        } else {
            _playerBody.drag = playerConstants.linearDrag * 0.15f;
        }
    }

    public void PlayerDeathResponse() {
        _playerAnimator.SetBool("onDeath", true);
    }
}