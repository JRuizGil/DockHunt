using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

public class SpawnPoolController : MonoBehaviour
{
    public SeagullPoolScriptable config;
    public GameObject seagullPrefab;
    public int poolSize = 1000;
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
            availableSeagull.gameObject.SetActive(true);
            spawnTransform = new Vector3(Random.Range(100, -100), Random.Range(500f,35f), Random.Range(0, -35));
            availableSeagull.ActivateSeagull(spawnTransform);
            yield return new WaitForSeconds(config.spawnInterval);
        }                
    }
}