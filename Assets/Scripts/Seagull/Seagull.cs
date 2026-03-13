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
    public Animator animator;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();    
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("isFalling", true);
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
        if (NavMesh.SamplePosition(startPos, out hit, 2000f, seagullAreaMask))
        {
            Vector3 targetGroundPos = hit.position;

            if (Vector3.Distance(startPos, targetGroundPos) <= 200f)
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
                if(animator.GetBool("isFalling")== false)
                {
                    animator.SetBool("isFleeing", false);
                    animator.SetBool("isFalling", true);
                }
                float speed = Mathf.Lerp(0.5f, config.flightSpeed, dist / 2f);

                transform.position = Vector3.MoveTowards(transform.position,targetGroundPos,speed * Time.deltaTime);
                
                yield return null;
            }
            if(animator.GetBool("isEating")== false)
                {
                    animator.SetBool("isFalling", false);
                    animator.SetBool("isEating", true);                    
                }
            if (isScared) yield return null;            
            //aqui espera un tiempo, pero si se asusta, interrumpe la espera y pasa a huir
            yield return WaitInterruptible(config.waitTime);
            //flees
            Debug.Log("Volviendo");              

            while (Vector3.Distance(transform.position, startPos) > 0.2f)
            {               
                if(animator.GetBool("isFleeing")== false)
                {
                animator.SetBool("isEating", false);
                animator.SetBool("isFleeing", true);
                }  
                transform.position = Vector3.MoveTowards(transform.position,startPos,config.flightSpeed * Time.deltaTime);
                yield return null;
            }
            gameObject.SetActive(false);
        }
        //si se asusta en cualquier momento, va a huir a la posición de spawn
        while (Vector3.Distance(transform.position, startPos) > 0.01f)
        {
            if(animator.GetBool("isFleeing") == false)
            {
            animator.SetBool("isFleeing", true);
            animator.SetBool("isEating", false);
            }
            transform.position = Vector3.MoveTowards(transform.position,startPos,config.flightSpeed * Time.deltaTime * 2);
            yield return null;
        }
        Debug.Log("Huido");
        gameObject.SetActive(false);
        if(animator.GetBool("isFleeing") == true)
        {
            animator.SetBool("isFleeing", false);
            animator.SetBool("isFalling", true);
        }

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