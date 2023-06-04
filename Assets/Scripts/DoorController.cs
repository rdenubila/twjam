using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    public Transform nextPosition;
    public Transform currentRoom;
    public Transform nextRoom;

    public bool isLocked = false;
    public GameObject roomToUnlock;

    void Start()
    {
        if (isLocked)
        {
            roomToUnlock.SetActive(false);
        }
    }

    public void EnterDoor(NavMeshAgent agent)
    {
        if (!isLocked)
        {
            agent.ResetPath();
            agent.Warp(nextPosition.position);

            currentRoom.gameObject.SetActive(false);
            nextRoom.gameObject.SetActive(true);
        }
        else
        {
            HandleLockedDoor();
        }
    }

    private void HandleLockedDoor()
    {
        GameController _gameController = FindObjectOfType<GameController>();
        bool hasKey = _gameController.HasGoal(Goals.ItemsKey);
        if (hasKey)
        {
            UnlockRoom();
        }
        else
        {
            _gameController.ShowNotification("DOOR_LOCKED");
        }
    }

    void UnlockRoom()
    {
        roomToUnlock.SetActive(true);
        Destroy(gameObject);
    }
}
