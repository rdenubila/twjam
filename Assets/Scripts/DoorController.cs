using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    public Transform nextPosition;
    public Transform currentRoom;
    public Transform nextRoom;

    public void EnterDoor(NavMeshAgent agent)
    {
        agent.ResetPath();
        agent.Warp(nextPosition.position);

        currentRoom.gameObject.SetActive(false);
        nextRoom.gameObject.SetActive(true);
    }
}
