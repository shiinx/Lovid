using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "ScriptableObjects/Variables/BoolVariable", order = 1)]
public class BoolVariable : ScriptableObject {
    public bool value;

    public bool Value {
        get => value;
        set => this.value = value;
    }
}