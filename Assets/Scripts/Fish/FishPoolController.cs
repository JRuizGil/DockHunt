using UnityEngine;
using System.Collections.Generic;

public class FishPoolController : MonoBehaviour
{
    public FishPoolScriptable config;
    public List<Fish> pool = new List<Fish>();
    public float currentday = 1;
    public float poolsize = 5;
    private void Awake()
    {
        poolsize = config.poolSize;
    }
    void SpawnFish()
    {
        for (int i = 0; i < poolsize; i++)
        {
            GameObject fshInstance = Instantiate(config.FishPrefab, transform);
            Fish s = fshInstance.GetComponent<Fish>();
            s.config = config;
            s.enabled = true;
            s.currentLife = config.lifepoints;

            Vector3 spawnPosition;
            bool validPosition;

            do
            {
                validPosition = true;
                spawnPosition = new Vector3(Random.Range(config.PosMin.x, config.PosMax.x), -0.02f, Random.Range(config.PosMin.z, config.PosMax.z));

                foreach (Fish fish in pool)
                {
                    if (Vector3.Distance(fish.transform.position, spawnPosition) < config.spawnAreaMargin)
                    {
                        validPosition = false;
                        break;
                    }
                }

            } while (!validPosition);

            fshInstance.transform.position = spawnPosition;

            fshInstance.SetActive(true);
            pool.Add(s);
        }
    }
    void OnEnable()
    {
        Restart();        
    }
    public void Restart()
    {
        // eliminar peces actuales
        foreach (Fish fish in pool)
        {
            if (fish != null)
                Destroy(fish.gameObject);
        }

        pool.Clear();

        // volver a generarlos
        SpawnFish();
        poolsize = poolsize + 3;

    }
    
}