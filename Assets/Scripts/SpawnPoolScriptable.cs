using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Seagullpool",menuName ="SeagullpoolScriptable")]
[System.Serializable]
public class SeagullPoolScriptable : ScriptableObject
{
    public float spawnInterval;
    public float inTime;
    public float stayTime;
    public float outTime;

}
