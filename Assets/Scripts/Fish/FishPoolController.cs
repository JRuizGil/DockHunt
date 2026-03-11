using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

public class FishPoolController : MonoBehaviour
{
    public FishPoolScriptable config;
    public List<Fish> pool = new List<Fish>();
    private void Start()
    {
        //instantiate all the fish
        for (int i = 0; i < config.poolSize; i++)
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
                spawnPosition = new Vector3(Random.Range(config.PosMin.x, config.PosMax.x),-0.02f,Random.Range(config.PosMin.z, config.PosMax.z));
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
}
