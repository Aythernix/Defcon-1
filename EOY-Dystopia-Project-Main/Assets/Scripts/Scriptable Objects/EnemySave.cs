using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySave", menuName = "Enemies/EnemySave", order = 2)]
public class EnemySave : ScriptableObject
{
    public List<Vector3> enemyTransforms;
    public List<float> enemyHealths;
}
