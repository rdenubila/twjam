using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum PiggyStates
{
    patrol, eating, attack
}

public class PiggyController : MonoBehaviour
{

    private NavMeshAgent agent;
    private GameController _gc;
    public PiggyStates currentState = PiggyStates.patrol;
    public List<Transform> patrolPoints;

    private float normalSpeed;
    private float normalAcceleration;
    private Transform player;
    public GameObject donutPrefab;
    public GameObject interactIcon;
    public UnityEvent OnEat;

    public float minDistance = 3.5f;

    void Awake()
    {
        _gc = FindObjectOfType<GameController>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        normalSpeed = agent.speed;
        normalAcceleration = agent.acceleration;

        patrolPoints = new List<Transform>(transform.parent.Find("Patrol Points").GetComponentsInChildren<Transform>());
        patrolPoints.RemoveAt(0);
        MoveToNextPatrolPoint();
    }

    bool ReachedDestination() => Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance;
    bool PlayerHasDonuts() => _gc.HasGoal(Goals.ItemsDonuts);
    bool isEating() => currentState == PiggyStates.eating;
    float distanceToPlayer() => Vector3.Distance(player.position, transform.position);
    bool isNearToPlayer() => distanceToPlayer() < minDistance;

    public bool CanFeed() => PlayerHasDonuts() && !isEating() && isNearToPlayer();

    void Update()
    {

        interactIcon.SetActive(CanFeed());

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
            player.GetComponent<CharacterMovement>().MoveTo(
                player.position + GetForwardDirectionRelativeTo(player, transform) * 3f
            );
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
        if (!isEating())
        {
            currentState = PiggyStates.attack;
            agent.speed = 50;
            agent.acceleration = 50;
            agent.SetDestination(player.position);
        }
    }

    public void Feed()
    {
        if (CanFeed())
        {
            GameObject donut = Instantiate(
                donutPrefab,
                player.position + GetForwardDirectionRelativeTo(transform, player),
                Quaternion.Euler(-90f, 0, 0)
                );
            currentState = PiggyStates.eating;
            OnEat?.Invoke();

            agent.stoppingDistance = 0.15f;
            agent.SetDestination(donut.transform.position);
        }
    }

    public Vector3 GetForwardDirectionRelativeTo(Transform targetObject, Transform referenceObject)
    {
        Vector3 direction = targetObject.position - referenceObject.position;
        direction.Normalize();
        return direction;
    }
}
