﻿using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "ScriptableObjects/Variables/IntVariable", order = 1)]
public class IntVariable : ScriptableObject {
    public int value;

    public int Value {
        get => value;
        set => this.value = value;
    }
}