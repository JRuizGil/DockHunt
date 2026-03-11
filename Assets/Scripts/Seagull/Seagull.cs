using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;
public class Seagull : MonoBehaviour
{
    public SeagullPoolScriptable config;
    private NavMeshAgent agent;
    private Vector3 spawnPoint;
    private bool isScared = false;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();    
        audioSource = GetComponent<AudioSource>();
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
    #region Activate seagull and call it
    public void ActivateSeagull(Vector3 startPos)
    {
        this.spawnPoint = startPos;
        transform.position = startPos;
        NavMeshHit hit;
        int seagullAreaMask = 1 << NavMesh.GetAreaFromName("Seagull");
        //if navmesh hits in a point closer than 20 units, saves and sends the corutine to that position
        if (NavMesh.SamplePosition(startPos, out hit, 100f, seagullAreaMask))
        {
            Vector3 targetGroundPos = hit.position;

            if (Vector3.Distance(startPos, targetGroundPos) <= 100f)
            {
                gameObject.SetActive(true);
                StartCoroutine(SeagullRoutine(startPos, targetGroundPos));
            }
        }
    }
    #endregion
    #region Seagull appears, eats and flees corutine
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
        while (Vector3.Distance(transform.position, startPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position,startPos,config.flightSpeed * Time.deltaTime * 2);
            yield return null;
        }
        Debug.Log("Huido");
        gameObject.SetActive(false);

    }
    #endregion
    #region funcion para poder interrumpir a la gaviota con ataque
    IEnumerator WaitInterruptible(float time)
    {
        float t = 0f;

        while (t < time)
        {
            if (isScared)
            {
                int i = Random.Range(0, audioClips.Length);
                audioSource.PlayOneShot(audioClips[i]);
                yield break; // salir inmediatamente
            }
            t += Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}