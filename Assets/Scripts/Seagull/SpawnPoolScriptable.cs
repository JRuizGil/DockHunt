using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Seagullpool",menuName ="SeagullpoolScriptable")]
[System.Serializable]
public class SeagullPoolScriptable : ScriptableObject
{
    public float spawnInterval = 0.5f;
    public float flightSpeed = 10f;
    public float waitTime = 100; 
}
