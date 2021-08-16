using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Texture2D platformTexture;
    public Texture2D attackTurretTexture;
    public Texture2D bombTurretTexture;
    public Texture2D defenseTurretTexture;
    
    public PlayerVariable playerVariable;
    // Start is called before the first frame update
    void Start() {
        Vector2 hotSpot = new Vector2(crosshairTexture.width / 2f, crosshairTexture.height / 2f);
        Cursor.SetCursor(crosshairTexture, hotSpot, CursorMode.Auto);
    }

    // Update is called once per frame
    public void EquipChangeResponse() {
        switch (playerVariable.PlayerEquipped) {
            case PlayerConstants.Primary.Gun: {
                Vector2 hotSpot = new Vector2(crosshairTexture.width / 2f, crosshairTexture.height / 2f);
                Cursor.SetCursor(crosshairTexture, hotSpot, CursorMode.Auto);
                break;
            }
            case PlayerConstants.Primary.Platform: {
                Vector2 hotSpot = new Vector2(platformTexture.width / 2f, platformTexture.height / 2f);
                Cursor.SetCursor(platformTexture, hotSpot, CursorMode.Auto);
                break;
            }
            case PlayerConstants.Primary.AttackTurret: {
                Vector2 hotSpot = new Vector2(attackTurretTexture.width / 2f, attackTurretTexture.height / 2f);
                Cursor.SetCursor(attackTurretTexture, hotSpot, CursorMode.Auto);
                break;
            }
            case PlayerConstants.Primary.BombTurret: {
                Vector2 hotSpot = new Vector2(bombTurretTexture.width / 2f, bombTurretTexture.height / 2f);
                Cursor.SetCursor(bombTurretTexture, hotSpot, CursorMode.Auto);
                break;
            }
            case PlayerConstants.Primary.DefenseTurret: {
                Vector2 hotSpot = new Vector2(defenseTurretTexture.width / 2f, defenseTurretTexture.height / 2f);
                Cursor.SetCursor(defenseTurretTexture, hotSpot, CursorMode.Auto);
                break;
            }
        }
    }
}
