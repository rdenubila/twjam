using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum PiggyStates
{
    patrol, eating, attack
}

public class PiggyController : MonoBehaviour
{

    private NavMeshAgent agent;
    public PiggyStates currentState = PiggyStates.patrol;
    public List<Transform> patrolPoints;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolPoints = new List<Transform>(transform.parent.Find("Patrol Points").GetComponentsInChildren<Transform>());
        patrolPoints.RemoveAt(0);
        MoveToNextPatrolPoint();
    }

    bool ReachedDestination() => Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance;

    void Update()
    {
        switch (currentState)
        {
            case PiggyStates.patrol:
                CheckPatrol();
                break;
            default:
                break;
        }
    }

    void CheckPatrol()
    {
        if (ReachedDestination())
        {
            MoveToNextPatrolPoint();
        }
    }

    private void MoveToNextPatrolPoint()
    {
        IEnumerable<Transform> points = patrolPoints.Where(point => Vector3.Distance(transform.position, point.position) > agent.stoppingDistance);
        Transform nextPoint = points.ElementAt(Random.Range(0, points.Count<Transform>()));
        agent.SetDestination(nextPoint.position);
    }
}
