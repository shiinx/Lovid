using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Primary", menuName = "ScriptableObjects/Variables/Primary", order = 3)]
public class Primary : ScriptableObject {
    public PlayerConstants.Primary value;

    public PlayerConstants.Primary Value {
        get => value;
        set => this.value = value;
    }
}