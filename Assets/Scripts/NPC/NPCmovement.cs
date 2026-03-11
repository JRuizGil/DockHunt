using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class NPCmovement : MonoBehaviour
{
    public NpcControllerScriptable config;
    public List<Transform> wayPoint;
    NavMeshAgent agent;

    public int CurrentWayPointIndex = 0;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        Walking();
    }
    void Walking()
    {
        if (wayPoint.Count == 0)
        {
            return;
        }
        float distancetowaypoint = Vector3.Distance(wayPoint[CurrentWayPointIndex].position, transform.position);
        if(distancetowaypoint <= 3)
        {
            CurrentWayPointIndex = (CurrentWayPointIndex + 1) % wayPoint.Count;
        }

        agent.SetDestination(wayPoint[CurrentWayPointIndex].position);

    }
}
