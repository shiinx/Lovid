using UnityEngine;
using UnityEngine.UI;

public class EnemyLeftTextController : MonoBehaviour {
    public IntVariable enemyLeft;

    private Text _enemyLeftText;

    private void Start() {
        _enemyLeftText = GetComponent<Text>();
        _enemyLeftText.text =  enemyLeft.Value + " Enemies Left";
    }

    public void WaveChangedResponse() {
        _enemyLeftText.text = enemyLeft.Value + " Enemies Left";
    }

    public void EnemyKilledResponse() {
        _enemyLeftText.text = enemyLeft.Value + " Enemies Left";
    }
}