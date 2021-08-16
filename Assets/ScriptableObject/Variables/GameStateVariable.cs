using UnityEngine;

[CreateAssetMenu(fileName = "GameStateVariable", menuName = "ScriptableObjects/Variables/GameStateVariable", order = 1)]
public class GameStateVariable : ScriptableObject {
    public GameConstants.GameState value;

    public GameConstants.GameState Value {
        get => value;
        set => this.value = value;
    }
}