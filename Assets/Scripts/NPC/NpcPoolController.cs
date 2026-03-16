using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPoolController : MonoBehaviour
{
    public NpcControllerScriptable config;
    public List<NPCmovement> pool = new List<NPCmovement>();
    public Vector3 RSpawn;
    public Vector3 LSpawn;
    public bool PointHour;

    private Coroutine spawnRoutine;
    private Coroutine activateRoutine;

    private void OnEnable()
    {
        Restart();
    }

    private void Awake()
    {
        // instantiate all the npcs and disable
        for (int i = 0; i < config.poolSize; i++)
        {
            GameObject npcInstance = Instantiate(config.NPCs[Random.Range(0, config.NPCs.Length)], transform);
            NPCmovement n = npcInstance.GetComponent<NPCmovement>();
            n.config = config;
            n.enabled = true;
            npcInstance.SetActive(false);
            pool.Add(n);
        }
    }

    public void Restart()
    {
        // parar corrutinas
        if (spawnRoutine != null) StopCoroutine(spawnRoutine);
        if (activateRoutine != null) StopCoroutine(activateRoutine);

        // resetear estado
        PointHour = false;

        // desactivar todos los NPC
        foreach (NPCmovement npc in pool)
        {
            if (npc.gameObject.activeSelf)
                npc.gameObject.SetActive(false);
        }

        // reiniciar sistema
        spawnRoutine = StartCoroutine(SpawnManager());
        activateRoutine = StartCoroutine(ActivateMore());
    }

    private IEnumerator SpawnManager()
    {
        while (true)
        {
            NPCmovement availableNPC = pool.Find(n => !n.gameObject.activeInHierarchy);
            if (availableNPC == null)
            {
                yield return null;
                continue;
            }

            Vector3 spawnTransformL = new Vector3(LSpawn.x, LSpawn.y, LSpawn.z + Random.Range(-10, 10));
            Vector3 spawnTransformR = new Vector3(RSpawn.x, RSpawn.y, RSpawn.z + Random.Range(-10, 10));

            bool spawnLeft = Random.value < 0.5f;

            Vector3 spawnPosition = spawnLeft ? spawnTransformL : spawnTransformR;
            Vector3 waypointPos = spawnLeft ? spawnTransformR : spawnTransformL;

            availableNPC.transform.position = spawnPosition;
            availableNPC.wayPoint = waypointPos;
            availableNPC.gameObject.SetActive(true);

            if (PointHour)
                yield return new WaitForSeconds(0.1f);
            else
                yield return new WaitForSeconds(config.spawnInterval);
        }
    }

    private IEnumerator ActivateMore()
    {
        PointHour = true;
        yield return new WaitForSeconds(5f);
        PointHour = false;
    }
}