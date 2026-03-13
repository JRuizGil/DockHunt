using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

public class SpawnPoolController : MonoBehaviour
{
    public SeagullPoolScriptable config;
    public GameObject seagullPrefab;
    public int poolSize = 100;
    public Vector3 spawnTransform;
    public Vector3 targetGroundPos;
    private List<Seagull> pool = new List<Seagull>();

    private void Start()
    {
        //instantiate al the seagulls and disble
        for (int i = 0; i < poolSize; i++)
        {
            GameObject sgInstance = Instantiate(seagullPrefab, transform);
            Seagull s = sgInstance.GetComponent<Seagull>();
            s.config = config;
            sgInstance.SetActive(false);
            pool.Add(s);
        }
        StartCoroutine(SpawnManager());
    }
    private IEnumerator SpawnManager()
    {
        //take the first disabled seagull from the pool, and activate it, and do the routine.
        while (true)
        {
            Seagull availableSeagull = pool.Find(s => !s.gameObject.activeInHierarchy);
            if (availableSeagull == null)
            {
                yield return null;
                continue;
            }
            availableSeagull.gameObject.SetActive(true);
            spawnTransform = new Vector3(Random.Range(-130f, 130f), Random.Range(80f,80f), Random.Range(-25f, 0f));
            availableSeagull.ActivateSeagull(spawnTransform);
            yield return new WaitForSeconds(config.spawnInterval);
        }                
    }
}