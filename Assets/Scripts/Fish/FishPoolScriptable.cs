using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
[CreateAssetMenu(fileName = "FishPoolScriptable", menuName = "Scriptable Objects/FishPoolScriptable")]
public class FishPoolScriptable : ScriptableObject
{
    public Vector3 PosMax;
    public Vector3 PosMin;
    public float lifepoints = 100f;
    public GameObject FishPrefab;
    public int poolSize = 5;
    public float spawnAreaMargin = 10f;
}
