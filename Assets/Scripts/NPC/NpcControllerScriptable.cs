using UnityEngine;

[CreateAssetMenu(fileName = "NpcControllerScriptable", menuName = "Scriptable Objects/NpcControllerScriptable")]
public class NpcControllerScriptable : ScriptableObject
{
    public float spawnInterval;
    public float poolSize;
    public Vector3 LSpawnPos;
    public Vector3 RSpawnPos;
    public GameObject[] NPCs;

}
