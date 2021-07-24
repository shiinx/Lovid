using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Variable", menuName = "ScriptableObjects/Variables/Vector2Variable", order = 4)]
public class Vector2Variable : ScriptableObject {
    public Vector2 value;

    public Vector2 Value {
        get => value;
        set => this.value = value;
    }
}