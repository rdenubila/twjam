using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent agent;
    private Animator anim;
    private DialogController dialogController;
    [SerializeField] private DoorController transportCharTo;
    [SerializeField] private ItemGrab grabItem;
    [SerializeField] private LayerMask clickableLayers;

    void Start()
    {
        dialogController = FindObjectOfType<DialogController>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (ShouldFreezeMovement()) return;

        MoveCharacter();
        CheckReachedDestination();
    }

    bool ShouldFreezeMovement() => dialogController.isDialogVisible();

    void CheckReachedDestination()
    {
        if (Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
        {
            if (transportCharTo)
            {
                transportCharTo.EnterDoor(agent);
            }
            if (grabItem)
            {
                grabItem.GrabItem();
            }
            ResetClickActions();

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

                Vector3 clickedPosition = hit.point;
                agent.SetDestination(clickedPosition);

                if (hit.collider.CompareTag("Door"))
                    OnDoorClick(hit.collider.gameObject);

                if (hit.collider.CompareTag("Piggy"))
                    FeedPiggy(hit.collider.gameObject);

                if (hit.collider.CompareTag("Item"))
                    grabItem = hit.collider.gameObject.GetComponent<ItemGrab>();

                if (hit.collider.CompareTag("NPC"))
                    StartDialogWithNPC(hit.collider.gameObject);

            }
        }
    }

    public void MoveTo(Vector3 to)
    {
        agent.SetDestination(to);
    }

    private void FeedPiggy(GameObject obj)
    {
        PiggyController piggy = obj.GetComponent<PiggyController>();
        piggy.Feed();
    }

    void ResetClickActions()
    {
        agent.ResetPath();
        transportCharTo = null;
        grabItem = null;
    }

    void OnDoorClick(GameObject obj)
    {
        transportCharTo = obj.GetComponent<DoorController>();

        if (transportCharTo.isLocked)
        {
            transportCharTo.EnterDoor(agent);
            transportCharTo = null;
            agent.SetDestination(transform.position);
        }
    }

    void StartDialogWithNPC(GameObject obj)
    {
        obj.GetComponent<DialogTrigger>()?.InitDialog();
    }
}
