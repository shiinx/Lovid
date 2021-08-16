using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "PathVariable", menuName = "ScriptableObjects/Variables/PathVariable", order = 5)]
public class PathVariable : ScriptableObject
{
    public float yTransform;
    public float xTransform;
    public float xStepValue;
    public EnemyConstants.PathType pathType;
    
    public float YTransform 
    {
        get => yTransform;
        set => this.YTransform = value;
    }

    public float XTransform
    {
        get => xTransform;
        set=> this.xTransform = value;
    }
    public float XStepValue
    {
        get => xStepValue;
        set => this.xStepValue = value;
    }
    public EnemyConstants.PathType PathType
    {
        get => pathType;
        set => this.pathType = value;
    }
}
