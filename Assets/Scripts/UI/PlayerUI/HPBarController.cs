using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour {
    public Slider slider;
    public FloatVariable health;
    public PlayerConstants playerConstants;

    private void Start() {
        slider.maxValue = playerConstants.maxHealth;
        slider.minValue = 0;
        slider.value = health.Value;
    }

    public void PlayerDamageResponse() {
        slider.value = health.Value;
    }
}