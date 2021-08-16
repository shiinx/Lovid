using UnityEngine;
using UnityEngine.UI;

public class HotKeyController : MonoBehaviour {
    public PlayerVariable playerVariable;
    public TurretConstants turretConstants;
    public IntVariable currentGameLevel;


    public GameObject bomb;
    
    public Outline gun;
    public Outline platform;
    public Outline attackTurret;
    public Outline bombTurret;
    public Outline defenseTurret;

    public Image attackDelayFill;
    public Image defenseDelayFill;
    public Image bombDelayFill;

    private void Start() {
        print(currentGameLevel.Value);
        bomb.SetActive(currentGameLevel.Value >= 2);
    }
    
    public void EquipChangeResponse() {
        switch (playerVariable.PlayerEquipped) {
            case PlayerConstants.Primary.Gun: {
                gun.effectColor = Color.red;
                platform.effectColor = Color.black;
                attackTurret.effectColor = Color.black;
                defenseTurret.effectColor = Color.black;
                bombTurret.effectColor = Color.black;
                break;
            }
            case PlayerConstants.Primary.Platform: {
                gun.effectColor = Color.black;
                platform.effectColor = Color.red;
                attackTurret.effectColor = Color.black;
                defenseTurret.effectColor = Color.black;
                bombTurret.effectColor = Color.black;
                break;
            }
            case PlayerConstants.Primary.AttackTurret: {
                gun.effectColor = Color.black;
                platform.effectColor = Color.black;
                attackTurret.effectColor = Color.red;
                defenseTurret.effectColor = Color.black;
                bombTurret.effectColor = Color.black;
                break;
            }
            case PlayerConstants.Primary.DefenseTurret: {
                gun.effectColor = Color.black;
                platform.effectColor = Color.black;
                attackTurret.effectColor = Color.black;
                defenseTurret.effectColor = Color.red;
                bombTurret.effectColor = Color.black;
                break;
            }
            case PlayerConstants.Primary.BombTurret: {
                gun.effectColor = Color.black;
                platform.effectColor = Color.black;
                attackTurret.effectColor = Color.black;
                defenseTurret.effectColor = Color.black;
                bombTurret.effectColor = Color.red;
                break;
            }
        }
    }

    private void Update() {
        if (playerVariable.AttackTurretDelayed) {
            attackDelayFill.fillAmount -= 1 / (turretConstants
                .attackTurretPlacementDelay / Time.deltaTime);
        }

        if (playerVariable.DefenseTurretDelayed) {
            defenseDelayFill.fillAmount -= 1 / (turretConstants
                .defenseTurretPlacementDelay / Time.deltaTime);
        }

        if (playerVariable.BombTurretDelayed) {
            bombDelayFill.fillAmount -= 1 / (turretConstants.bombTurretPlacementDelay /
                                             Time.deltaTime);
        }
        
    }

    public void AttackTurretPlaced() {
        attackDelayFill.fillAmount = 1;
    }
    
    public void DefenseTurretPlaced() {
        defenseDelayFill.fillAmount = 1;
    }
    
    public void BombTurretPlaced() {
        bombDelayFill.fillAmount = 1;
    }

}