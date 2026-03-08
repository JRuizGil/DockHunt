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
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Attack"))
        {
            Debug.Log("Asustada");
            isScared=true;
        }
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
            //moves to navmesh closest point whith lerp
            while (Vector3.Distance(transform.position, targetGroundPos) > 0.1f)
            {
                float dist = Vector3.Distance(transform.position, targetGroundPos);

                float speed = Mathf.Lerp(0.5f, config.flightSpeed, dist / 2f);

                transform.position = Vector3.MoveTowards(transform.position,targetGroundPos,speed * Time.deltaTime);

                yield return null;
            }
            if (isScared) yield return null;
            yield return WaitInterruptible(config.waitTime);
            //flees
            Debug.Log("Volviendo");
            while (Vector3.Distance(transform.position, startPos) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position,startPos,config.flightSpeed * Time.deltaTime);
                yield return null;
            }
        }
        Debug.Log("Huyendo");
        transform.position = Vector3.MoveTowards(transform.position, startPos, config.flightSpeed * 10f * Time.deltaTime);
        gameObject.SetActive(false);
        yield return null;        
        
    }
    IEnumerator WaitInterruptible(float time)
    {
        float t = 0f;

        while (t < time)
        {
            if (isScared)
                yield break; // salir inmediatamente

            t += Time.deltaTime;
            yield return null;
        }
    }
}