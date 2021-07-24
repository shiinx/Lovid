using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "ScriptableObjects/Variables/FloatVariable", order = 2)]
public class FloatVariable : ScriptableObject {
    public float value;

    public float Value {
        get => value;
        set => this.value = value;
    }
}