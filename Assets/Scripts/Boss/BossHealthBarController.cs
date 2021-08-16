using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BossHealthBarController : MonoBehaviour
{
    public Slider slider;
    public FloatVariable health;
    public List<BossConstants> bossConstants;
    public IntVariable bossNumber ; 

    private void Start() { 
        slider.maxValue =  bossConstants[bossNumber.Value].maxHealth ; 
        slider.minValue = 0;
        slider.value = health.Value;
    }

    public void resetValues(){
        slider.maxValue =  bossConstants[bossNumber.Value].maxHealth ; 
        slider.minValue = 0;
        slider.value = health.Value;
    }

    public void BossDamageResponse() {
        slider.value = health.Value;
    }
    
}