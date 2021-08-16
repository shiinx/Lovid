using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/Constants/GameConstants")]
public class GameConstants : ScriptableObject {
    public enum GameState {
        MainMenu,
        Build,
        Level1Wave1,
        Level1Wave2,
        Level1Wave3,
        Level1Boss1,
        Level1Boss2,
        Win,
        Lose, 
        Reset
    }
}

