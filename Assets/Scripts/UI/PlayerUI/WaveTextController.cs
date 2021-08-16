using UnityEngine;
using UnityEngine.UI;

public class WaveTextController : MonoBehaviour {
    public IntVariable lastWave;
    public IntVariable currentWave;
    private Text _waveText;

    private void Start() {
        _waveText = GetComponent<Text>();
        _waveText.text = currentWave.Value + "/" + lastWave.Value;
    }

    public void WaveChangedResponse() {
        _waveText.text = currentWave.Value + "/" + lastWave.Value;
    }
}