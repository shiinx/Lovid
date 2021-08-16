using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBarController : MonoBehaviour {
    private Slider _slider;
    public PlayerVariable playerVariable;
    public PlayerConstants playerConstants;

    private void Start() {
        _slider = GetComponent<Slider>();
        _slider.maxValue = playerConstants.maxHealth;
        _slider.minValue = 0;
        _slider.value = playerVariable.PlayerHealth;
    }

    public void PlayerHPChange() {
        _slider.value = playerVariable.PlayerHealth;
    }
    
}