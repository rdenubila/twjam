using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemGrab : MonoBehaviour
{
    public Goals type;
    public GameObject interactIcon;
    private Transform player;
    public float minDistance = 3.5f;
    public bool canGrab = true;
    public UnityEvent onTryGrab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    float distanceToPlayer() => Vector3.Distance(player.position, transform.position);
    bool isNearToPlayer() => distanceToPlayer() < minDistance;

    void Update()
    {
        interactIcon.SetActive(isNearToPlayer());
    }

    public void GrabItem()
    {
        if (canGrab)
        {
            FindObjectOfType<GameController>().AddGoal(type);
            Destroy(gameObject);
        }
        else
        {
            onTryGrab.Invoke();
        }
    }
}
