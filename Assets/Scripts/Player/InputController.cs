using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour {

    public PlayerVariable playerVariable;
    
    public IntVariable waveNumber;
    public BoolVariable isGamePausedVariable;

    public IntVariable currentGameLevel;
    
    public GameObject platform;
    public GameObject attackTurret;
    public GameObject bombTurret;
    public GameObject defenseTurret;

    public GameObject ground;
    
    public PlayerConstants playerConstants;
    public TurretConstants turretConstants;
    
    public UnityEvent onPlayerBuild;
    public UnityEvent onPlayerEquipChange;
    public UnityEvent onPlayerShoot;

    public UnityEvent onAttackBuild;
    public UnityEvent onDefenseBuild;
    public UnityEvent onBombBuild;
    
    private GameObject _gun;
    private Camera _camera;
    private bool _isPaused;
    private float _nextFire;
    private float _firingRate;
    private Transform _marioTransform;

    // Start is called before the first frame update
    private void Start() {
        _camera = Camera.main;
        _marioTransform = GetComponent<Transform>();
        _gun = transform.GetChild(0).gameObject;
        _nextFire = Time.time;
    }

    // Update is called once per frame
    private void Update() {
        if (waveNumber.Value == -1 || isGamePausedVariable.Value)
            return;
        
        HotKeyHandler();
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.y < ground.transform.position.y) {
            return;
        }
        var currentEq = playerVariable.PlayerEquipped;
        if (currentEq == PlayerConstants.Primary.Gun) {
            if (Input.GetMouseButton(0) & Time.time > _nextFire) {
                _nextFire = Time.time + playerVariable.PlayerFiringDelay;
                onPlayerShoot.Invoke();
            }
        } else {
            if (Input.GetMouseButtonDown(0) && !playerVariable.PlayerSilenced && IsNearToPlayer(mousePosition))
                PlaceConstruct(mousePosition, currentEq);   
        }

        if (Input.GetMouseButtonDown(1) && IsNearToPlayer(mousePosition)) {
            var hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit) {
                var obj = hit.transform.root.gameObject;
                DestroyObj(obj);
            }
        }
        
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

    private bool IsOverlap(Vector2 mousePosition, float margin) {
        // TODO: Overlap handling
        // var hit = Physics2D.OverlapCircleAll(mousePosition, margin);
        // print(hit);
        // foreach (var VARIABLE in hit) {
        //     print(VARIABLE.name);
        // }
        return false;
    }

    private bool HaveEnoughCurrency(int buildCost) {
        return playerVariable.PlayerCurrency - buildCost >= 0;
    }

    private void PlaceConstruct(Vector2 mousePosition, PlayerConstants.Primary currentEq) {
        switch (currentEq) {
            case PlayerConstants.Primary.Platform: {
                var buildCost = playerConstants.platformBuildCost;
                var margin = playerConstants.platformMarginSpace;
                if (HaveEnoughCurrency(buildCost) && !IsOverlap(mousePosition, margin)) {
                    Instantiate(platform, mousePosition, Quaternion.identity);
                    playerVariable.PlayerCurrency -= buildCost;
                }

                break;
            }
            case PlayerConstants.Primary.AttackTurret: {
                var buildCost = turretConstants.attackTurretBuildCost;
                var margin = turretConstants.attackTurretMarginSpace;
                if (HaveEnoughCurrency(buildCost) && !IsOverlap(mousePosition, margin) 
                && !playerVariable.AttackTurretDelayed) {
                    StartCoroutine(AttackDelay());
                    Instantiate(attackTurret, mousePosition, Quaternion.identity);
                    playerVariable.PlayerCurrency -= buildCost;
                }
                break;
            }
            case PlayerConstants.Primary.DefenseTurret: {
                var buildCost = turretConstants.defenseTurretBuildCost;
                var margin = turretConstants.defenseTurretMarginSpace;
                if (HaveEnoughCurrency(buildCost) && !IsOverlap(mousePosition, margin) 
                && !playerVariable.DefenseTurretDelayed) {
                    StartCoroutine(DefenseDelay());
                    Instantiate(defenseTurret, mousePosition, Quaternion.identity);
                    playerVariable.PlayerCurrency -= buildCost;
                }
                break;
            }
            case PlayerConstants.Primary.BombTurret: {
                var buildCost = turretConstants.bombTurretBuildCost;
                var margin = turretConstants.bombTurretMarginSpace;
                if (HaveEnoughCurrency(buildCost) && !IsOverlap(mousePosition, margin) 
                && !playerVariable.BombTurretDelayed) {
                    StartCoroutine(BombDelay());
                    Instantiate(bombTurret, mousePosition, Quaternion.identity);
                    playerVariable.PlayerCurrency -= buildCost;
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
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            playerVariable.PlayerEquipped = PlayerConstants.Primary.Gun;
            _gun.SetActive(true);
            onPlayerEquipChange.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            playerVariable.PlayerEquipped = PlayerConstants.Primary.Platform;
            _gun.SetActive(false);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            onPlayerEquipChange.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            playerVariable.PlayerEquipped = PlayerConstants.Primary.AttackTurret;
            _gun.SetActive(false);
            onPlayerEquipChange.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            playerVariable.PlayerEquipped = PlayerConstants.Primary.DefenseTurret;
            _gun.SetActive(false);
            onPlayerEquipChange.Invoke();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha5) && currentGameLevel.Value == 2) {
            playerVariable.PlayerEquipped = PlayerConstants.Primary.BombTurret;
            _gun.SetActive(false);
            onPlayerEquipChange.Invoke();
        }
    }

    private void DestroyObj(GameObject obj) {
        // Handles destroying of turret
        // TODO: Confirm whether we are returning money or not
        switch (obj.tag) {
            case "Platform": {
                var buildCost = playerConstants.platformBuildCost;
                // buildLimit.Value -= buildCost;
                break;
            }
            case "AttackTurret": {
                var buildCost = turretConstants.attackTurretBuildCost;
                // buildLimit.Value -= buildCost;
                break;
            }
            case "BombTurret": {
                var buildCost = turretConstants.bombTurretBuildCost;
                // buildLimit.Value -= buildCost;
                break;
            }
            case "DefenseTurret": {
                var buildCost = turretConstants.defenseTurretBuildCost;
                // buildLimit.Value -= buildCost;
                break;
            }
        
            default: {
                return;
            }
        }

        Destroy(obj);
        //onPlayerBuild.Invoke();
    }
    
    private IEnumerator AttackDelay() {
        playerVariable.AttackTurretDelayed = true;
        onAttackBuild.Invoke();
        yield return new WaitForSeconds(turretConstants.attackTurretPlacementDelay);
        playerVariable.AttackTurretDelayed = false;
    }
    
    private IEnumerator DefenseDelay() {
        playerVariable.DefenseTurretDelayed = true;
        onDefenseBuild.Invoke();
        yield return new WaitForSeconds(turretConstants.defenseTurretPlacementDelay);
        playerVariable.DefenseTurretDelayed = false;
    }
    
    private IEnumerator BombDelay() {
        playerVariable.BombTurretDelayed = true;
        onBombBuild.Invoke();
        yield return new WaitForSeconds(turretConstants.bombTurretPlacementDelay);
        playerVariable.BombTurretDelayed = false;
    }

}