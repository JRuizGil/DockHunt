using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

public class SpawnPoolController : MonoBehaviour
{
    public SeagullPoolScriptable config;
    public GameObject seagullPrefab;
    public int poolSize = 80;
    public float spawnInterval;
    public Vector3 spawnTransform;
    public Vector3 targetGroundPos;
    public List<Seagull> pool = new List<Seagull>();
    private Coroutine spawnRoutine;

    private void Awake()
    {
        spawnInterval = config.spawnInterval;
    }
    void OnEnable()
    {
        Restart();
    }
    void OnDisable()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }
    private void Start()
    { 
        spawnRoutine = StartCoroutine(SpawnManager());
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
            spawnTransform = new Vector3(Random.Range(-100f, 100f), Random.Range(80f,80f), Random.Range(-25f, 0f));
            availableSeagull.ActivateSeagull(spawnTransform);
            yield return new WaitForSeconds(spawnInterval);
        }                
    }
    public void Restart()
    {
        // parar spawn
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        // desactivar todas las gaviotas
        foreach (Seagull s in pool)
        {
            if (s.gameObject.activeSelf)
                s.gameObject.SetActive(false);
        }
        // reset variables si hace falta
        spawnTransform = Vector3.zero;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject sgInstance = Instantiate(seagullPrefab, transform);
            Seagull s = sgInstance.GetComponent<Seagull>();
            s.config = config;
            sgInstance.SetActive(false);
            pool.Add(s);
        }
        poolSize += 40;
        // reiniciar spawn
        spawnRoutine = StartCoroutine(SpawnManager());
    }
}