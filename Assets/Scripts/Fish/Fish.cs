using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
public class Fish : MonoBehaviour
{
    public FishPoolScriptable config;
    public GameObject[] Fishes;
    public GameObject Exclamation;
    public float currentSeagullDamage;
    public float currentLife;
    public bool hasSeagulls;
    private float[] lifeThresholds = { 50f, 100f, 150f };

    public void Start()
    {
        Exclamation.SetActive(false);
        StartCoroutine(ReduceLifeRoutine());
    }
    //rutina que quita vida si hay gaviotas
    IEnumerator ReduceLifeRoutine()
    {
        while (true)
        {
            if (hasSeagulls)
            {
                currentLife -= currentSeagullDamage;

                for (int i = 0; i < Fishes.Length; i++)
                {
                    if (i < lifeThresholds.Length && currentLife < lifeThresholds[i] && Fishes[i].activeInHierarchy)
                    {
                        Fishes[i].SetActive(false);
                    }
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
    //comprueba si hay gaviotas dentro
    public void CheckContent()
    {
        if(currentSeagullDamage == 0)
        {
            hasSeagulls = false;
            Exclamation.SetActive(false);
        }
        else
        {
            hasSeagulls = true;
            Exclamation.SetActive(true);
        }
    }
    #region logica de quitar vida con colliders
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Seagull"))
        {
            currentSeagullDamage++;
            CheckContent();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Seagull"))
        {
            currentSeagullDamage--;
            CheckContent();
        }
    }
    #endregion
}
