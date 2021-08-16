using UnityEngine;
using UnityEngine.UI;

public class CurrencyTextController : MonoBehaviour {

    public PlayerVariable playerVariable;
    private Text _currencyText;

    // Start is called before the first frame update
    private void Start() {
        _currencyText = GetComponent<Text>();
        _currencyText.text = playerVariable.PlayerCurrency.ToString();
    }

    public void PlayerBuildObj() {
        _currencyText.text = playerVariable.PlayerCurrency.ToString();
    }
}