using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField] private DoorController transportCharTo;
    [SerializeField] private LayerMask clickableLayers;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        SetAnimationByVelocity();
        CheckReachedDestination();
    }

    void CheckReachedDestination()
    {
        if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
        {
            if (transportCharTo)
            {
                transportCharTo.EnterDoor(agent);
                ResetClickActions();

            }
        }
    }

    void MoveCharacter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ResetClickActions();
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 15f, clickableLayers))
            {

                if (hit.collider.CompareTag("Door"))
                    OnDoorClick(hit.collider.gameObject);

                Vector3 clickedPosition = hit.point;
                agent.SetDestination(clickedPosition);
            }
        }
    }

    void ResetClickActions()
    {
        agent.ResetPath();
        transportCharTo = null;
    }

    void OnDoorClick(GameObject obj)
    {
        transportCharTo = obj.GetComponent<DoorController>();
    }

    void SetAnimationByVelocity()
    {
        Vector3 agentVelocity = OffsetByAngle(agent.velocity.normalized, -45f);
        anim.SetFloat("velX", agentVelocity.x);
        anim.SetFloat("velY", -agentVelocity.z);
    }

    Vector3 OffsetByAngle(Vector3 normalizedVector, float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        float rotatedX = normalizedVector.x * Mathf.Cos(angleRad) - normalizedVector.z * Mathf.Sin(angleRad);
        float rotatedZ = normalizedVector.x * Mathf.Sin(angleRad) + normalizedVector.z * Mathf.Cos(angleRad);

        Vector3 rotatedVector = new Vector3(rotatedX, 0f, rotatedZ);
        return rotatedVector;
    }
}
