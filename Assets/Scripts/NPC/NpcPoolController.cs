using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcPoolController : MonoBehaviour
{
    public NpcControllerScriptable config;
    public List<NPCmovement> pool = new List<NPCmovement>();

    private void Start()
    {
        //instantiate al the npcs and disble
        for (int i = 0; i < config.poolSize; i++)
        {
            GameObject npcInstance = Instantiate(config.NPCs[Random.Range(0,config.NPCs.Length)], transform);
            NPCmovement n = npcInstance.GetComponent<NPCmovement>();
            n.config = config;
            n.enabled = true;
            npcInstance.SetActive(false);
            pool.Add(n);
        }
        StartCoroutine(SpawnManager());
    }
    private IEnumerator SpawnManager()
    {
        //take the first disabled npc from the pool, and activate it, and do the routine.
        while (true)
        {
            NPCmovement availableNPC = pool.Find(n=> !n.gameObject.activeInHierarchy);
            availableNPC.gameObject.SetActive(true);
            Vector3 spawnTransform = new Vector3(Random.Range(70, -70), Random.Range(70, 35f), Random.Range(0, -35));
            //availableNPC.ActivateSeagull(spawnTransform);
            yield return new WaitForSeconds(config.spawnInterval);
        }
    }
}
