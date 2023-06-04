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

    private float normalSpeed;
    private float normalAcceleration;
    private Transform player;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        normalSpeed = agent.speed;
        normalAcceleration = agent.acceleration;

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
            case PiggyStates.attack:
                CheckAttack();
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

    void CheckAttack()
    {
        if (ReachedDestination())
        {
            currentState = PiggyStates.patrol;
            agent.speed = normalSpeed;
            agent.acceleration = normalAcceleration;
            agent.ResetPath();
            agent.velocity = agent.velocity * .2f;
            MoveToNextPatrolPoint();
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    private void MoveToNextPatrolPoint()
    {
        IEnumerable<Transform> points = patrolPoints.Where(point => Vector3.Distance(transform.position, point.position) > agent.stoppingDistance);
        Transform nextPoint = points.ElementAt(Random.Range(0, points.Count<Transform>()));
        agent.SetDestination(nextPoint.position);
    }

    public void AttackPlayer()
    {
        currentState = PiggyStates.attack;
        agent.speed = 50;
        agent.acceleration = 50;
        agent.SetDestination(player.position);
    }
}
