using UnityEngine;
using UnityEngine.UI;

public class FlagHPBarController : MonoBehaviour
{
    public Slider slider;
    public FloatVariable health;
    public FlagConstants flagConstants;

    private void Start() {
        slider.maxValue = flagConstants.startingFlagHealth;
        slider.minValue = 0;
        slider.value = health.Value;
    }

    public void FlagDamageResponse() {
        slider.value = health.Value;
    }
    
}
