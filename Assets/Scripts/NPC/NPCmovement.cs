using UnityEngine;
using UnityEngine.AI;

public class NPCmovement : MonoBehaviour
{
    public NpcControllerScriptable config;
    public Vector3 wayPoint;
    public NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(wayPoint);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            gameObject.SetActive(false);
        }
    }
}