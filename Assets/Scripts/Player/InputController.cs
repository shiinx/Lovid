using System;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour {
    public Primary equipped;
    public IntVariable buildLimit;
    public Vector2Variable direction;
    public GameObject platform;
    public GameObject attackTurret;
    public GameObject bombTurret;
    public GameObject defenseTurret;
    public GameObject bullet;
    public PlayerConstants playerConstants;
    public TurretConstants turretConstants;
    public LevelConstants levelConstants;
    public UnityEvent onPlayerBuild;
    private Camera _camera;

    private Transform _marioTransform;

    // Start is called before the first frame update
    private void Start() {
        _camera = Camera.main;
        _marioTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update() {
        HotKeyHandler();

        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var currentEq = equipped.Value;
            if (currentEq == PlayerConstants.Primary.Gun) {
                Shoot(mousePosition);
            } else {
                if (IsNearToPlayer(mousePosition)) PlaceConstruct(mousePosition, currentEq);
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit) {
                var obj = hit.transform.gameObject;
                DestroyObj(obj);
            }
        }
    }

    private void Shoot(Vector2 mousePosition) {
        var position = _marioTransform.position;
        direction.Value = mousePosition - new Vector2(position.x, position.y);
        Instantiate(bullet, position, Quaternion.identity);
    }

    private bool IsNearToPlayer(Vector2 mousePosition) {
        var position = _marioTransform.position;
        var x = Mathf.Abs(mousePosition.x - position.x);
        var y = Mathf.Abs(mousePosition.y - position.y);
        var dis = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        if (dis <= playerConstants.constructDistance)
            return true;
        return false;
    }

    private bool IsWithinBuildLimit(int buildCost) {
        return buildLimit.Value + buildCost <= levelConstants.buildLimit;
    }

    private bool IsOverlap(Vector2 mousePosition, float margin) {
        // TODO: Overlap handling
        // var hit = Physics2D.OverlapCircleAll(mousePosition, margin);
        // print(hit);
        // foreach (var VARIABLE in hit) {
        //     print(VARIABLE.name);
        // }
        return false;
    }

    private void PlaceConstruct(Vector2 mousePosition, PlayerConstants.Primary currentEq) {
        switch (currentEq) {
            case PlayerConstants.Primary.Platform: {
                var buildCost = playerConstants.platformBuildCost;
                var margin = playerConstants.platformMarginSpace;
                if (IsWithinBuildLimit(buildCost) && !IsOverlap(mousePosition, margin)) {
                    Instantiate(platform, mousePosition, Quaternion.identity);
                    buildLimit.Value += buildCost;
                }

                break;
            }
            case PlayerConstants.Primary.AttackTurret: {
                var buildCost = turretConstants.attackTurretBuildCost;
                var margin = turretConstants.attackTurretMarginSpace;
                if (IsWithinBuildLimit(buildCost) && !IsOverlap(mousePosition, margin)) {
                    Instantiate(attackTurret, mousePosition, Quaternion.identity);
                    buildLimit.Value += buildCost;
                }

                break;
            }
            case PlayerConstants.Primary.BombTurret: {
                var buildCost = turretConstants.bombTurretBuildCost;
                var margin = turretConstants.bombTurretMarginSpace;
                if (IsWithinBuildLimit(buildCost) && !IsOverlap(mousePosition, margin)) {
                    Instantiate(bombTurret, mousePosition, Quaternion.identity);
                    buildLimit.Value += buildCost;
                }

                break;
            }
            case PlayerConstants.Primary.DefenseTurret: {
                var buildCost = turretConstants.defenseTurretBuildCost;
                var margin = turretConstants.defenseTurretMarginSpace;
                if (IsWithinBuildLimit(buildCost) && !IsOverlap(mousePosition, margin)) {
                    Instantiate(defenseTurret, mousePosition, Quaternion.identity);
                    buildLimit.Value += buildCost;
                }

                break;
            }
            case PlayerConstants.Primary.Gun:
                print("Entered impossible switch section: gun");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(currentEq), currentEq, null);
        }
        onPlayerBuild.Invoke();
    }

    private void HotKeyHandler() {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            equipped.Value = PlayerConstants.Primary.Gun;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            equipped.Value = PlayerConstants.Primary.Platform;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            equipped.Value = PlayerConstants.Primary.AttackTurret;

        if (Input.GetKeyDown(KeyCode.Alpha4))
            equipped.Value = PlayerConstants.Primary.BombTurret;

        if (Input.GetKeyDown(KeyCode.Alpha5))
            equipped.Value = PlayerConstants.Primary.DefenseTurret;
    }

    private void DestroyObj(GameObject obj) {
        switch (obj.tag) {
            case "Platform": {
                var buildCost = playerConstants.platformBuildCost;
                buildLimit.Value -= buildCost;
                break;
            }
            case "AttackTurret": {
                var buildCost = turretConstants.attackTurretBuildCost;
                buildLimit.Value -= buildCost;
                break;
            }
            case "DefenseTurret": {
                var buildCost = turretConstants.defenseTurretBuildCost;
                buildLimit.Value -= buildCost;
                break;
            }
            case "BombTurret": {
                var buildCost = turretConstants.bombTurretBuildCost;
                buildLimit.Value -= buildCost;
                break;
            }
            default: {
                return;
            }
        }

        Destroy(obj);
        onPlayerBuild.Invoke();
    }
}