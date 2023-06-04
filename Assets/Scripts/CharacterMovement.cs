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
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
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
                if (hit.collider.CompareTag("NPC"))
                    StartDialogWithNPC(hit.collider.gameObject);

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

    void StartDialogWithNPC(GameObject obj)
    {
        obj.GetComponent<DialogTrigger>()?.InitDialog();
    }
}
