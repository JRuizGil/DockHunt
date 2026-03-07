using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
public class Seagull : MonoBehaviour
{
    public SeagullPoolScriptable config;
    private NavMeshAgent agent;
    private Vector3 spawnPoint;
    private bool isScared = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();        
        agent.enabled = false;
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    public void ActivateSeagull(Vector3 startPos)
    {
        this.spawnPoint = startPos;
        transform.position = startPos;
        NavMeshHit hit;
        
        //if navmesh hits in a point closer than 20 units, saves and sends the corutine to that position
        if (NavMesh.SamplePosition(startPos, out hit, 100f, NavMesh.AllAreas))
        {
            Vector3 targetGroundPos = hit.position;
            //Debug.Log("Start: " + startPos);
            //Debug.Log("Target: " + targetGroundPos);
            if (Vector3.Distance(startPos, targetGroundPos) <= 100f)
            {
                gameObject.SetActive(true);                
                StartCoroutine(SeagullRoutine(startPos, targetGroundPos));
            }
        }
    }

    private IEnumerator SeagullRoutine(Vector3 startPos,Vector3 targetGroundPos)
    {
        Debug.Log("inicia la corutina de la gaviota");
        while (!isScared)
        {
            while (Vector3.Distance(transform.position, targetGroundPos) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetGroundPos, config.flightSpeed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(config.waitTime);
            while (Vector3.Distance(transform.position, startPos) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, config.flightSpeed * Time.deltaTime);
                yield return null;
            }
            gameObject.SetActive(false);
            yield return null;
        }
        while (Vector3.Distance(transform.position, startPos) > 0.2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, config.flightSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }    
}