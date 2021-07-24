using UnityEngine;
using UnityEngine.UI;

public class HotKeyController : MonoBehaviour {
    public Primary equipped;

    public Outline gun;
    public Outline platform;
    public Outline attackTurret;
    public Outline bombTurret;
    public Outline defenseTurret;

    public void EquipChangeResponse() {
        switch (equipped.Value) {
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
            case PlayerConstants.Primary.BombTurret: {
                gun.effectColor = Color.black;
                platform.effectColor = Color.black;
                attackTurret.effectColor = Color.black;
                defenseTurret.effectColor = Color.red;
                bombTurret.effectColor = Color.black;
                break;
            }
            case PlayerConstants.Primary.DefenseTurret: {
                gun.effectColor = Color.black;
                platform.effectColor = Color.black;
                attackTurret.effectColor = Color.black;
                defenseTurret.effectColor = Color.black;
                bombTurret.effectColor = Color.red;
                break;
            }
        }
    }
}