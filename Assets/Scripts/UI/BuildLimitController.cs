using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildLimitController : MonoBehaviour {
    
    public Text buildLimitText;

    public LevelConstants levelConstants;

    public IntVariable buildLimit;
    // Start is called before the first frame update
    void Start() {
        buildLimitText = GetComponent<Text>();
        var maxBuildLimit = levelConstants.buildLimit;
        buildLimitText.text = "0 / " + maxBuildLimit;
    }

    public void PlayerBuildObj() {
        var maxBuildLimit = levelConstants.buildLimit;
        buildLimitText.text = buildLimit.Value + " / " + maxBuildLimit;
    }
}
